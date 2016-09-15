#region using directives

using System;
using System.Collections.Generic;
using Google.Protobuf;
using PokemonGo.RocketAPI.Enums;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Platform;
using POGOProtos.Networking.Platform.Requests;
using POGOProtos.Networking.Requests;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class RequestBuilder
    {
        private static readonly Random RandomDevice = new Random();
        private readonly double _altitude;
        private readonly AuthTicket _authTicket;
        private readonly string _authToken;
        private readonly AuthType _authType;
        private readonly Crypt _crypt;
        private readonly double _horizontalAccuracy;
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly float _speed;
        private readonly ISettings _settings;
        private readonly int _startTime;
        private ulong _nextRequestId;

        public RequestBuilder(string authToken, AuthType authType, double latitude, double longitude, double altitude, float speed,
            ISettings settings, AuthTicket authTicket = null)
        {
            _authToken = authToken;
            _authType = authType;
            _latitude = latitude;
            _longitude = longitude;
            _altitude = altitude;
            if (speed == 0)
                _speed = (float)Math.Round(GenRandom(4, 8), 7); // Range 4-8
            else
                _speed = speed;
            _horizontalAccuracy = (float) Math.Round(GenRandom(50, 250), 7);
            _settings = settings;
            _authTicket = authTicket;
            _nextRequestId = Convert.ToUInt64(RandomDevice.NextDouble()*Math.Pow(10, 18));
            if (_startTime == 0)
                _startTime = Utils.GetTime(true);

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

        private RequestEnvelope.Types.PlatformRequest GenerateSignature(IEnumerable<IMessage> requests)
        {
            var ticketBytes = _authTicket.ToByteArray();

            Signature.Types.DeviceInfo deviceInfo;
            if (_settings.HardwareManufacturer.Equals("Apple", StringComparison.Ordinal))
            {
                // iOS
                deviceInfo = new Signature.Types.DeviceInfo
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
            }
            else
            {
                // Android
                deviceInfo = new Signature.Types.DeviceInfo
                {
                    DeviceId = _settings.DeviceId,
                    AndroidBoardName = _settings.AndroidBoardName,
                    AndroidBootloader = _settings.AndroidBootloader,
                    DeviceBrand = _settings.DeviceBrand,
                    DeviceModel = _settings.DeviceModel,
                    DeviceModelIdentifier = _settings.DeviceModelIdentifier,
                    DeviceModelBoot = _settings.DeviceModelBoot,
                    HardwareManufacturer = _settings.HardwareManufacturer,
                    HardwareModel = _settings.HardwareModel,
                    FirmwareBrand = _settings.FirmwareBrand,
                    FirmwareTags = _settings.FirmwareTags,
                    FirmwareType = _settings.FirmwareType,
                    FirmwareFingerprint = _settings.FirmwareFingerprint
                };
            }

            var sig = new Signature
            {
                Timestamp = (ulong) Utils.GetTime(true),
                TimestampSinceStart = (ulong) (Utils.GetTime(true) - _startTime),
                LocationHash1 = Utils.GenerateLocation1(ticketBytes, _latitude, _longitude, _horizontalAccuracy),
                LocationHash2 = Utils.GenerateLocation2(_latitude, _longitude, _horizontalAccuracy),
                SensorInfo = new Signature.Types.SensorInfo
                {
                    TimestampSnapshot = (ulong) (Utils.GetTime(true) - _startTime - RandomDevice.Next(100, 400)),
                    LinearAccelerationX = GenRandom(-0.31110161542892456, 0.1681540310382843),
                    LinearAccelerationY = GenRandom(-0.6574847102165222, -0.07290205359458923),
                    LinearAccelerationZ = GenRandom(-0.9943905472755432, -0.7463029026985168),
                    MagneticFieldX = GenRandom(-0.139084026217, 0.138112977147),
                    MagneticFieldY = GenRandom(-0.2, 0.19),
                    MagneticFieldZ = GenRandom(-0.2, 0.4),
                    RotationVectorX = GenRandom(-47.149471283, 61.8397789001),
                    RotationVectorY = GenRandom(-47.149471283, 61.8397789001),
                    RotationVectorZ = GenRandom(-47.149471283, 5),
                    GyroscopeRawX = GenRandom(0.0729667818829, 0.0729667818829),
                    GyroscopeRawY = GenRandom(-2.788630499244109, 3.0586791383810468),
                    GyroscopeRawZ = GenRandom(-0.34825887123552773, 0.19347580173737935),
                    GravityX = GenRandom(-0.9703824520111084, 0.8556089401245117),
                    GravityY = GenRandom(-1.7470258474349976, 1.4218578338623047),
                    GravityZ = GenRandom(-0.9681901931762695, 0.8396636843681335),
                    AccelerometerAxes = 3
                },
                DeviceInfo = deviceInfo
            };

            Signature.Types.LocationFix locationFix = new Signature.Types.LocationFix
            {
                Provider = "fused",
                Latitude = (float)_latitude,
                Longitude = (float)_longitude,
                Altitude = (float)_altitude,
                HorizontalAccuracy = (float)_horizontalAccuracy,
                TimestampSnapshot = (ulong)(Utils.GetTime(true) - _startTime - RandomDevice.Next(100, 300)),
                ProviderStatus = 3,
                LocationType = 1
            };

            if (_settings.DevicePlatform.Equals("ios", StringComparison.Ordinal))
            {
                // Vertical accuracy is iOS only.
                locationFix.VerticalAccuracy = RandomDevice.Next(10, 12); // Range is 10-12

                // Course is iOS only.
                locationFix.Course = (float)Math.Round(GenRandom(0, 360), 7); // Range is 0-360

                // Speed is iOS only.
                locationFix.Speed = _speed;
            }

            sig.LocationFix.Add(locationFix);

            foreach (var request in requests)
                sig.RequestHash.Add(Utils.GenerateRequestHash(ticketBytes, request.ToByteArray()));

            sig.SessionHash = SessionHash;
            //sig.Unknown25 = -8537042734809897855; // For 0.33
            sig.Unknown25 = 7363665268261373700; // For 0.35

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

        public RequestEnvelope GetRequestEnvelope(params Request[] customRequests)
        {
            var e = new RequestEnvelope
            {
                StatusCode = 2, //1

                RequestId = _nextRequestId++, //3
                Requests = {customRequests}, //4

                //Unknown6 = , //6
                Latitude = _latitude, //7
                Longitude = _longitude, //8
                Accuracy = _horizontalAccuracy, //9
                AuthTicket = _authTicket, //11
                MsSinceLastLocationfix = RandomDevice.Next(800, 1900) //12
            };
            e.PlatformRequests.Add(GenerateSignature(customRequests));
            return e;
        }

        public RequestEnvelope GetInitialRequestEnvelope(params Request[] customRequests)
        {
            var e = new RequestEnvelope
            {
                StatusCode = 2, //1

                RequestId = _nextRequestId++, //3
                Requests = {customRequests}, //4

                //Unknown6 = , //6
                Latitude = _latitude, //7
                Longitude = _longitude, //8
                Accuracy = _horizontalAccuracy, //9
                AuthInfo = new RequestEnvelope.Types.AuthInfo
                {
                    Provider = _authType == AuthType.Google ? "google" : "ptc",
                    Token = new RequestEnvelope.Types.AuthInfo.Types.JWT
                    {
                        Contents = _authToken,
                        Unknown2 = 59
                    }
                }, //10
                MsSinceLastLocationfix = RandomDevice.Next(800, 1900) //12
            };
            return e;
        }

        public RequestEnvelope GetRequestEnvelope(RequestType type, IMessage message)
        {
            return GetRequestEnvelope(new Request
            {
                RequestType = type,
                RequestMessage = message.ToByteString()
            });
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