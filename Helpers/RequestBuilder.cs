﻿#region using directives

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
using System.Text;
using static POGOProtos.Networking.Envelopes.Signature.Types;
using System.Threading.Tasks;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class RequestBuilder
    {
        /* The next variables are specific to 51.0 client.
         * /!\ old 
         * private static long Client_4500_Unknown25 = -1553869577012279119;
         * /!\ New */
        private static long Client_5100_Unknown25 = -1553869577012279119;
        private static readonly Random RandomDevice = new Random();
        private static readonly TRandom TRandomDevice = new TRandom();
        private readonly double _altitude;
        private readonly AuthType _authType;
        private readonly Crypt _crypt;
        private readonly int _horizontalAccuracy;
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly float _speed;
        private readonly Client _client;
        private readonly ISettings _settings;
        
        private float _course = RandomDevice.Next(0, 360);
        private int _token2 = RandomDevice.Next(1, 59);
        
        public RequestBuilder(Client client, AuthType authType, double latitude, double longitude, double altitude, float speed,
            ISettings settings)
        {
            _client = client;
            _authType = authType;
            _latitude = latitude;
            _longitude = longitude;
            _altitude = altitude;

            // Add small variance to speed.
            _speed = speed + ((float)Math.Round(GenRandom(-1, 1), 7));

            // If speed is 0 or negative, make it random.
            if (_speed <= 0)
                _speed = (float)TRandomDevice.Triangular(0.1, 3.1, .8);

            _horizontalAccuracy = TRandomDevice.Choice(new List<int>(new int[] { 5, 5, 5, 5, 10, 10, 10 }));

            _settings = settings;

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

        public static uint RequestCount { get; private set; } = 1;
        private readonly Random _random = new Random(Environment.TickCount);

        private long PositiveRandom()
        {
            long ret = _random.Next() | (_random.Next() << 32);
            // lrand48 ensures it's never < 0
            // So do the same
            if (ret < 0)
                ret = -ret;
            return ret;
        }

        public static void Reset()
        {
            RequestCount = 0;
        } 

        private void IncrementRequestCount()
        {
            // Request counts on android jump more than 1 at a time according to logs
            // They are fully sequential on iOS though
            // So mimic that same behavior here.
            if (_client.Platform == Platform.Android)
                RequestCount += (uint)_random.Next(2, 15);
            else if (_client.Platform == Platform.Ios)
                RequestCount++;
        }

        private ulong GetNextRequestId()
        {
            if (RequestCount == 1)
            {
                IncrementRequestCount();
                if (_client.Platform == Platform.Android)
                {
                    // lrand48 is "broken" in that the first run of it will return a static value.
                    // So the first time we send a request, we need to match that initial value. 
                    // Note: On android srand(4) is called in .init_array which seeds the initial value.
                    return 0x53B77E48000000B0;
                }
                if (_client.Platform == Platform.Ios)
                {
                    // Same as lrand48, iOS uses "rand()" without a pre-seed which always gives the same first value.
                    return 0x41A700000002;
                }
            }

            // Note that the API expects a "positive" random value here. (At least on Android it does due to lrand48 implementation details)
            // So we'll just use the same for iOS since it doesn't hurt, and means less code required.
            ulong r = (((ulong)PositiveRandom() | ((RequestCount + 1) >> 31)) << 32) | (RequestCount + 1);
            IncrementRequestCount();
            return r;
        }

        private RequestEnvelope.Types.PlatformRequest GenerateSignature(RequestEnvelope requestEnvelope)
        {
            byte[] ticketBytes = requestEnvelope.AuthTicket != null ? requestEnvelope.AuthTicket.ToByteArray() : requestEnvelope.AuthInfo.ToByteArray();

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
                Unknown25 = Client_5100_Unknown25,
                Timestamp = (ulong)Utils.GetTime(true),
                TimestampSinceStart = (ulong)(Utils.GetTime(true) - _client.StartTime),
                LocationHash1 = (int)Utils.GenerateLocation1(ticketBytes, _latitude, _longitude, _horizontalAccuracy),
                LocationHash2 = (int)Utils.GenerateLocation2(_latitude, _longitude, _horizontalAccuracy),
                DeviceInfo = deviceInfo
            };

            sig.SensorInfo.Add(new SensorInfo()
            {
                TimestampSnapshot = (ulong)(Utils.GetTime(true) - _client.StartTime - RandomDevice.Next(100, 500)),
                LinearAccelerationX = TRandomDevice.Triangular(-3, 1, 0),
                LinearAccelerationY = TRandomDevice.Triangular(-2, 3, 0),
                LinearAccelerationZ = TRandomDevice.Triangular(-4, 2, 0),
                MagneticFieldX = TRandomDevice.Triangular(-50, 50, 0),
                MagneticFieldY = TRandomDevice.Triangular(-60, 50, -5),
                MagneticFieldZ = TRandomDevice.Triangular(-60, 40, -30),
                AttitudePitch = GenRandom(-47.149471283, 61.8397789001),
                AttitudeYaw = GenRandom(-47.149471283, 61.8397789001),
                AttitudeRoll = GenRandom(-47.149471283, 5),
                RotationRateX = GenRandom(0.0729667818829, 0.0729667818829),
                RotationRateY = GenRandom(-2.788630499244109, 3.0586791383810468),
                RotationRateZ = GenRandom(-0.34825887123552773, 0.19347580173737935),
                GravityX = TRandomDevice.Triangular(-1, 1, 0.15),
                GravityY = TRandomDevice.Triangular(-1, 1, -.2),
                GravityZ = TRandomDevice.Triangular(-1, .7, -0.8),
                MagneticFieldAccuracy = -1,
                Status = 3
            });

            Signature.Types.LocationFix locationFix = new Signature.Types.LocationFix
            {
                Provider = TRandomDevice.Choice(new List<string>(new string[] { "network", "network", "network", "network", "fused" })),
                Latitude = (float)_latitude,
                Longitude = (float)_longitude,
                Altitude = (float)_altitude,
                HorizontalAccuracy = _horizontalAccuracy,
                TimestampSnapshot = (ulong)(Utils.GetTime(true) - _client.StartTime - RandomDevice.Next(100, 300)),
                ProviderStatus = 3,
                LocationType = 1
            };
            
            if (_horizontalAccuracy >= 65)
            {
                locationFix.HorizontalAccuracy = TRandomDevice.Choice(new List<int>(new int[] { _horizontalAccuracy, 65, 65, (int)Math.Round(GenRandom(66, 80)), 200 }));
                if (_client.Platform == Platform.Ios)
                    locationFix.VerticalAccuracy = (float)TRandomDevice.Triangular(35, 100, 65);
            }
            else
            {
                locationFix.HorizontalAccuracy = _horizontalAccuracy;
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

            foreach (var request in requestEnvelope.Requests)
                sig.RequestHash.Add(Utils.GenerateRequestHash(ticketBytes, request.ToByteArray()));
            
            var encryptedSignature = new RequestEnvelope.Types.PlatformRequest
            {
                Type = PlatformRequestType.SendEncryptedSignature,
                RequestMessage = new SendEncryptedSignatureRequest
                {
                    EncryptedSignature = ByteString.CopyFrom(PCrypt.Encrypt(sig.ToByteArray(), (uint)_client.StartTime))
                }.ToByteString()
            };

            return encryptedSignature;
        }

        public async Task<RequestEnvelope> GetRequestEnvelope(Request[] customRequests)
        {
            var e = new RequestEnvelope
            {
                StatusCode = 2, //1
                RequestId = GetNextRequestId(), //3
                Latitude = _latitude, //7
                Longitude = _longitude, //8
                Accuracy = (int)_horizontalAccuracy, //9
                MsSinceLastLocationfix = (long)TRandomDevice.Triangular(300, 30000, 10000) //12
            };

            e.Requests.AddRange(customRequests);
            
            if (_client.AccessToken.AuthTicket == null || 
                (_client.AccessToken.AuthTicket != null && _client.AccessToken.AuthTicket.ExpireTimestampMs < (ulong)Utils.GetTime(true) - (60000 * 10)) || // Check AuthTicket expiration (with 10 minute buffer)
                _client.AccessToken.IsExpired)
            {
                if (_client.AccessToken.IsExpired)
                {
                    await Rpc.Login.Reauthenticate(_client);
                }

                e.AuthInfo = new RequestEnvelope.Types.AuthInfo
                {
                    Provider = _client.AccessToken.ProviderID,
                    Token = new RequestEnvelope.Types.AuthInfo.Types.JWT
                    {
                        Contents = _client.AccessToken.Token,
                        Unknown2 = 59
                    }
                };
            }
            else
            {
                e.AuthTicket = _client.AccessToken.AuthTicket;
            }

            e.PlatformRequests.Add(GenerateSignature(e));

            return e;
        }
        
        public async Task<RequestEnvelope> GetRequestEnvelope(RequestType type, IMessage message)
        {
            return await GetRequestEnvelope(new Request[] { new Request
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