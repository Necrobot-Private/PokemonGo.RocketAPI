#region using directives

using System;
using System.Collections.Generic;
using Google.Protobuf;
using PokemonGo.RocketAPI.Enums;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Platform;
using POGOProtos.Networking.Platform.Requests;
using POGOProtos.Networking.Requests;
using POGOProtos.Enums;
using Troschuetz.Random;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class RequestBuilder
    {
        // The next variables are specific to 0.35 client.
        private static long Client_3500_Unknown25 = 7363665268261373700;
        private static int  Client_3500_InitialRequestIdConstant_Android = 1404534344; //0x53B77E48
        private static int  Client_3500_InitialRequestIdConstant_Ios = 16807; //0x000041A7

        private static readonly Random RandomDevice = new Random();
        private static readonly TRandom TRandomDevice = new TRandom();
        private readonly double _altitude;
        private readonly AuthTicket _authTicket;
        private readonly string _authToken;
        private readonly AuthType _authType;
        private readonly Crypt _crypt;
        private readonly double _horizontalAccuracy;
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly float _speed;
        private readonly Client _client;
        private readonly ISettings _settings;
        
        private static long NextRequestId;
        private float _course = RandomDevice.Next(0, 360);
        private int _token2 = RandomDevice.Next(1, 59);
        
        public RequestBuilder(Client client, string authToken, AuthType authType, double latitude, double longitude, double altitude, float speed,
            ISettings settings, AuthTicket authTicket = null)
        {
            _client = client;
            _authToken = authToken;
            _authType = authType;
            _latitude = latitude;
            _longitude = longitude;
            _altitude = altitude;

            // Add small variance to speed.
            _speed = speed + ((float)Math.Round(GenRandom(-1, 1), 7));

            // If speed is 0 or negative, make it random.
            if (_speed <= 0)
                _speed = (float)TRandomDevice.Triangular(0.1, 3.1, .8);

            _horizontalAccuracy = TRandomDevice.Choice(new List<double>(new double[] { 5, 5, 5, 5, 10, 10, 10, 30, 30, 50, 65, GenRandom(66, 80) }));

            _settings = settings;
            _authTicket = authTicket;

            if (SessionHash == null)
            {
                GenerateNewHash();
            }

            if (_crypt == null)
                _crypt = new Crypt();
        }

        private ByteString SessionHash
        {
            get { return _settings.SessionHash; }
            set { _settings.SessionHash = value; }
        }

        public void GenerateNewHash()
        {
            var hashBytes = new byte[16];

            RandomDevice.NextBytes(hashBytes);

            SessionHash = ByteString.CopyFrom(hashBytes);
        }

        public ulong GenerateRequestId()
        {
            int rand;
            if (NextRequestId == 0) // Startup
            {
                NextRequestId = 1;

                if (_client.Platform == Platform.Ios)
                    rand = Client_3500_InitialRequestIdConstant_Ios;
                else
                    rand = Client_3500_InitialRequestIdConstant_Android;
            }
            else
            {
                rand = RandomDevice.Next(0, Int32.MaxValue);
            }
            NextRequestId += 1;
            var cnt = NextRequestId;
            var reqId = ((Convert.ToInt64(rand) | ((cnt & -1) >> 31)) << 32) | cnt;
            return Convert.ToUInt64(reqId);
        }

        private RequestEnvelope.Types.PlatformRequest GenerateSignature(IEnumerable<IMessage> requests)
        {
            var ticketBytes = _authTicket.ToByteArray();

            // Common device info
            Signature.Types.DeviceInfo deviceInfo = new Signature.Types.DeviceInfo
            {
                DeviceId = _settings.DeviceId,
                DeviceBrand = _settings.DeviceBrand,
                DeviceModel = _settings.DeviceModel,
                DeviceModelBoot = _settings.DeviceModelBoot,
                HardwareManufacturer = _settings.HardwareManufacturer,
                HardwareModel = _settings.HardwareModel,
                FirmwareBrand = _settings.FirmwareBrand,
                FirmwareType = _settings.FirmwareType
            };

            // Android
            if (_client.Platform == Platform.Android)
            {
                deviceInfo.AndroidBoardName = _settings.AndroidBoardName;
                deviceInfo.AndroidBootloader = _settings.AndroidBootloader;
                deviceInfo.DeviceModelIdentifier = _settings.DeviceModelIdentifier;
                deviceInfo.FirmwareTags = _settings.FirmwareTags;
                deviceInfo.FirmwareFingerprint = _settings.FirmwareFingerprint;
            }

            var sig = new Signature
            {
                SessionHash = SessionHash,
                Unknown25 = Client_3500_Unknown25,
                Timestamp = (ulong) Utils.GetTime(true),
                TimestampSinceStart = (ulong) (Utils.GetTime(true) - _client.StartTime),
                LocationHash1 = Utils.GenerateLocation1(ticketBytes, _latitude, _longitude, _horizontalAccuracy),
                LocationHash2 = Utils.GenerateLocation2(_latitude, _longitude, _horizontalAccuracy),
                SensorInfo = new Signature.Types.SensorInfo
                {
                    TimestampSnapshot = (ulong) (Utils.GetTime(true) - _client.StartTime - RandomDevice.Next(100, 500)),
                    LinearAccelerationX = TRandomDevice.Triangular(-3, 1, 0),
                    LinearAccelerationY = TRandomDevice.Triangular(-2, 3, 0),
                    LinearAccelerationZ = TRandomDevice.Triangular(-4, 2, 0),
                    MagneticFieldX = TRandomDevice.Triangular(-50, 50, 0),
                    MagneticFieldY = TRandomDevice.Triangular(-60, 50, -5),
                    MagneticFieldZ = TRandomDevice.Triangular(-60, 40, -30),
                    RotationVectorX = GenRandom(-47.149471283, 61.8397789001),
                    RotationVectorY = GenRandom(-47.149471283, 61.8397789001),
                    RotationVectorZ = GenRandom(-47.149471283, 5),
                    GyroscopeRawX = GenRandom(0.0729667818829, 0.0729667818829),
                    GyroscopeRawY = GenRandom(-2.788630499244109, 3.0586791383810468),
                    GyroscopeRawZ = GenRandom(-0.34825887123552773, 0.19347580173737935),
                    GravityX = TRandomDevice.Triangular(-1, 1, 0.15),
                    GravityY = TRandomDevice.Triangular(-1, 1, -.2),
                    GravityZ = TRandomDevice.Triangular(-1, .7, -0.8),
                    AccelerometerAxes = 3
                },
                DeviceInfo = deviceInfo
            };

            Signature.Types.LocationFix locationFix = new Signature.Types.LocationFix
            {
                Provider = TRandomDevice.Choice(new List<string>(new string[] { "network", "network", "network", "network", "fused" })),
                Latitude = (float)_latitude,
                Longitude = (float)_longitude,
                Altitude = (float)_altitude,
                HorizontalAccuracy = (float)_horizontalAccuracy,
                TimestampSnapshot = (ulong)(Utils.GetTime(true) - _client.StartTime - RandomDevice.Next(100, 300)),
                ProviderStatus = 3,
                LocationType = 1
            };

            if (_horizontalAccuracy >= 65)
            {
                locationFix.HorizontalAccuracy = (float)TRandomDevice.Choice(new List<double>(new double[] { _horizontalAccuracy, 65, 65, GenRandom(66, 80), 200 }));
                if (_client.Platform == Platform.Ios)
                    locationFix.VerticalAccuracy = (float)TRandomDevice.Triangular(35, 100, 65);
            }
            else
            {
                locationFix.HorizontalAccuracy = (float)_horizontalAccuracy;
                if (_client.Platform == Platform.Ios)
                {
                    if (_horizontalAccuracy > 10)
                        locationFix.VerticalAccuracy = (float)TRandomDevice.Choice(new List<double>(new double[] { 24, 32, 48, 48, 64, 64, 96, 128 }));
                    else
                        locationFix.VerticalAccuracy = (float)TRandomDevice.Choice(new List<double>(new double[] { 3, 4, 6, 6, 8, 12, 24 }));
                }
            }

            if (_client.Platform == Platform.Ios)
            {
                if (RandomDevice.NextDouble() > 0.95)
                {
                    // No reading for roughly 1 in 20 updates
                    locationFix.Course = -1;
                    locationFix.Speed = -1;
                }
                else
                {
                    _course = (float)TRandomDevice.Triangular(0, 360, _course);

                    // Course is iOS only.
                    locationFix.Course = _course;

                    // Speed is iOS only.
                    locationFix.Speed = _speed;
                }
            }

            sig.LocationFix.Add(locationFix);

            foreach (var request in requests)
                sig.RequestHash.Add(Utils.GenerateRequestHash(ticketBytes, request.ToByteArray()));
            
            var encryptedSignature = new RequestEnvelope.Types.PlatformRequest
            {
                Type = PlatformRequestType.SendEncryptedSignature,
                RequestMessage = new SendEncryptedSignatureRequest
                {
                    EncryptedSignature = ByteString.CopyFrom(_crypt.Encrypt(sig.ToByteArray()))
                }.ToByteString()
            };

            return encryptedSignature;
        }

        public RequestEnvelope GetRequestEnvelope(Request[] customRequests, bool isInitialRequest = false)
        {
            var e = new RequestEnvelope
            {
                StatusCode = 2, //1
                RequestId = GenerateRequestId(), //3
                Requests = {customRequests}, //4
                Latitude = _latitude, //7
                Longitude = _longitude, //8
                Accuracy = _horizontalAccuracy, //9
                AuthTicket = _authTicket, //11
                MsSinceLastLocationfix = (long)TRandomDevice.Triangular(300, 30000, 10000) //12
            };

            if (_authTicket != null && !isInitialRequest)
            {
                e.AuthTicket = _authTicket;
                e.PlatformRequests.Add(GenerateSignature(customRequests));
            }
            else
            {
                e.AuthInfo = new RequestEnvelope.Types.AuthInfo
                {
                    Provider = _authType == AuthType.Google ? "google" : "ptc",
                    Token = new RequestEnvelope.Types.AuthInfo.Types.JWT
                    {
                        Contents = _authToken,
                        Unknown2 = _token2
                    }
                }; //10
            }
            
            return e;
        }
        
        public RequestEnvelope GetRequestEnvelope(RequestType type, IMessage message)
        {
            return GetRequestEnvelope(new Request[] { new Request
            {
                RequestType = type,
                RequestMessage = message.ToByteString()
            } });
        }

        public static double GenRandom(double num)
        {
            const float randomFactor = 0.3f;
            var randomMin = num*(1 - randomFactor);
            var randomMax = num*(1 + randomFactor);
            var randomizedDelay = RandomDevice.NextDouble()*(randomMax - randomMin) + randomMin;
            return randomizedDelay;
        }

        public static double GenRandom(double min, double max)
        {
            return RandomDevice.NextDouble()*(max - min) + min;
        }
    }
}