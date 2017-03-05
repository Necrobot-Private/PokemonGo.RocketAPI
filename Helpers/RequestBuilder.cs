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
using static POGOProtos.Networking.Envelopes.Signature.Types;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Hash;
using Newtonsoft.Json;
using GeoCoordinatePortable;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class RequestBuilder
    {
        private Random RandomDevice;
        private TRandom TRandomDevice;
        private LehmerRng _lehmerRng;
        private readonly Client _client;
        private readonly ISettings _settings;
        private ByteString _sessionHash;
        private int _requestCount;
        private float _course;

        public RequestBuilder(Client client, ISettings settings)
        {
            RandomDevice = new Random();
            TRandomDevice = new TRandom();
            _client = client;
            _settings = settings;
            _lehmerRng = new LehmerRng();

            if (_sessionHash == null)
                GenerateNewHash();
            
            _requestCount = 1;
            _course = (float)GenRandom(0, 359.9);
        }
        
        public void GenerateNewHash()
        {
            var hashBytes = new byte[16];
            RandomDevice.NextBytes(hashBytes);

            _sessionHash = ByteString.CopyFrom(hashBytes);
        }
        
        public int GetNextRequestId()
        {
            _requestCount += 1;
            var r = _lehmerRng.Next();
            var nextRequestId = (r << 32) | _requestCount;
            return nextRequestId;
        }

        private float GetCourse()
        {
            _course = (float)TRandomDevice.Triangular(0, 359.9, _course);
            return _course;
        }

        private RequestEnvelope.Types.PlatformRequest GenerateSignature(RequestEnvelope requestEnvelope, GeoCoordinate currentLocation)
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
                SessionHash = _sessionHash,
                Unknown25 = _client.Unknown25,
                Timestamp = (ulong)Utils.GetTime(true),
                TimestampSinceStart = (ulong)(Utils.GetTime(true) - _client.StartTime),
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
                MagneticFieldAccuracy = TRandomDevice.Choice(new List<int>(new int[] { -1, 1, 1, 2, 2, 2, 2 })),
                AttitudePitch = TRandomDevice.Triangular(-1.5, 1.5, 0.2),
                AttitudeYaw = GenRandom(-3, 3),
                AttitudeRoll = TRandomDevice.Triangular(-2.8, 2.5, 0.25),
                RotationRateX = TRandomDevice.Triangular(-6, 4, 0),
                RotationRateY = TRandomDevice.Triangular(-5.5, 5, 0),
                RotationRateZ = TRandomDevice.Triangular(-5, 3, 0),
                GravityX = TRandomDevice.Triangular(-1, 1, 0.15),
                GravityY = TRandomDevice.Triangular(-1, 1, -.2),
                GravityZ = TRandomDevice.Triangular(-1, .7, -0.8),
                Status = 3
            });

            Signature.Types.LocationFix locationFix = new Signature.Types.LocationFix
            {
                Provider = TRandomDevice.Choice(new List<string>(new string[] { "network", "network", "network", "network", "fused" })),
                Latitude = (float)currentLocation.Latitude,
                Longitude = (float)currentLocation.Longitude,
                Altitude = (float)currentLocation.Altitude,
                TimestampSnapshot = (ulong)(Utils.GetTime(true) - _client.StartTime - RandomDevice.Next(100, 300)),
                ProviderStatus = 3,
                LocationType = 1
            };

            if (requestEnvelope.Accuracy >= 65)
            {
                locationFix.HorizontalAccuracy = TRandomDevice.Choice(new List<float>(new float[] { (float)requestEnvelope.Accuracy, 65, 65, (int)Math.Round(GenRandom(66, 80)), 200 }));
                if (_client.Platform == Platform.Ios)
                    locationFix.VerticalAccuracy = (float)TRandomDevice.Triangular(35, 100, 65);
            }
            else
            {
                locationFix.HorizontalAccuracy = (float)requestEnvelope.Accuracy;
                if (_client.Platform == Platform.Ios)
                {
                    if (requestEnvelope.Accuracy > 10)
                        locationFix.VerticalAccuracy = (float)TRandomDevice.Choice(new List<double>(new double[] { 24, 32, 48, 48, 64, 64, 96, 128 }));
                    else
                        locationFix.VerticalAccuracy = (float)TRandomDevice.Choice(new List<double>(new double[] { 3, 4, 6, 6, 8, 12, 24 }));
                }
            }
            
            if (_client.Platform == Platform.Ios)
            {
                sig.ActivityStatus = new ActivityStatus();
                sig.ActivityStatus.Stationary = true;
                if (RandomDevice.NextDouble() > 0.50)
                {
                    sig.ActivityStatus.Tilting = true;
                }

                if (RandomDevice.NextDouble() > 0.95)
                {
                    // No reading for roughly 1 in 20 updates
                    locationFix.Course = -1;
                    locationFix.Speed = -1;
                }
                else
                {
                    // Course is iOS only.
                    locationFix.Course = GetCourse();

                    // Speed is iOS only.
                    locationFix.Speed = (float)TRandomDevice.Triangular(0.2, 4.25, 1);
                }
            }

            sig.LocationFix.Add(locationFix);
            
            string envelopString = JsonConvert.SerializeObject(requestEnvelope);

            HashRequestContent hashRequest = new HashRequestContent()
            {
                Latitude = currentLocation.Latitude,
                Longitude = currentLocation.Longitude,
                Altitude = requestEnvelope.Accuracy,
                AuthTicket = ticketBytes,
                SessionData = _sessionHash.ToByteArray(),
                Requests = new List<byte[]>(),
                Timestamp = sig.Timestamp
            };


            foreach (var request in requestEnvelope.Requests)
            {
                hashRequest.Requests.Add(request.ToByteArray());
            }

            var res = _client.Hasher.RequestHashesAsync(hashRequest).Result;

            foreach (var item in res.RequestHashes)
            {
                sig.RequestHash.Add((unchecked((ulong) item)));
            }
            sig.LocationHash1 = unchecked((int)res.LocationAuthHash);
            sig.LocationHash2 = unchecked((int)res.LocationHash);

            var encryptedSignature = new RequestEnvelope.Types.PlatformRequest
            {
                Type = PlatformRequestType.SendEncryptedSignature,
                RequestMessage = new SendEncryptedSignatureRequest
                {
                    EncryptedSignature = ByteString.CopyFrom(_client.Cryptor.Encrypt(sig.ToByteArray(), (uint)_client.StartTime))
                }.ToByteString()
            };

            return encryptedSignature;
        }

        public async Task RegenerateRequestEnvelopeWithNewAccessToken(RequestEnvelope requestEnvelope)
        {
            var accessToken = await Rpc.Login.GetValidAccessToken(_client, true /* force refresh */);

            requestEnvelope.AuthTicket = null;
            requestEnvelope.AuthInfo = new RequestEnvelope.Types.AuthInfo
            {
                Provider = accessToken.ProviderID,
                Token = new RequestEnvelope.Types.AuthInfo.Types.JWT
                {
                    Contents = accessToken.Token,
                    Unknown2 = (accessToken.ProviderID == "ptc") ? TRandomDevice.Choice(new List<int>(new int[] { 0, 21, 28, 28, 56, 59, 59, 59 })) : 0
                }
            };

            requestEnvelope.PlatformRequests.Clear();

            if (_client.AppVersion > 4500)
            {
                // Only add UnknownPtr8Request if not using the legacy API.
                // Chat with SLxTnT - this is required for all request and needed before the main envelope.

                //if(customRequests.Any(x=>x.RequestType == RequestType.GetMapObjects  || x.RequestType == RequestType.GetPlayer))
                var plat8Message = new UnknownPtr8Request()
                {
                    Message = _client.UnknownPlat8Field
                };
                requestEnvelope.PlatformRequests.Add(new RequestEnvelope.Types.PlatformRequest()
                {
                    Type = PlatformRequestType.UnknownPtr8,
                    RequestMessage = plat8Message.ToByteString()
                });
            }

            var currentLocation = new GeoCoordinate(requestEnvelope.Latitude, requestEnvelope.Longitude, _client.CurrentAltitude);
            requestEnvelope.PlatformRequests.Add(GenerateSignature(requestEnvelope, currentLocation));
        }

        public async Task<RequestEnvelope> GetRequestEnvelope(IEnumerable<Request> customRequests)
        {
            // Save the location
            GeoCoordinate currentLocation = new GeoCoordinate(_client.CurrentLatitude, _client.CurrentLongitude, _client.CurrentAltitude);

            var e = new RequestEnvelope
            {
                StatusCode = 2, //1
                RequestId = (ulong)GetNextRequestId(), //3
                Latitude = currentLocation.Latitude, //7
                Longitude = currentLocation.Longitude, //8
                Accuracy = TRandomDevice.Choice(new List<int>(new int[] { 5, 5, 5, 5, 10, 10, 10, 30, 30, 50, 65, RandomDevice.Next(66, 80) })), //9
                MsSinceLastLocationfix = (long)TRandomDevice.Triangular(300, 30000, 10000) //12
            };

            e.Requests.AddRange(customRequests);
            
            if (_client.AuthTicket != null)
            {
                e.AuthTicket = _client.AuthTicket;
            }
            else
            {
                var accessToken = await Rpc.Login.GetValidAccessToken(_client);
                e.AuthInfo = new RequestEnvelope.Types.AuthInfo
                {
                    Provider = accessToken.ProviderID,
                    Token = new RequestEnvelope.Types.AuthInfo.Types.JWT
                    {
                        Contents = accessToken.Token,
                        Unknown2 = (accessToken.ProviderID == "ptc") ? TRandomDevice.Choice(new List<int>(new int[] { 0, 21, 28, 28, 56, 59, 59, 59 })) : 0
                    }
                };
            }

            if (_client.AppVersion > 4500)
            {
                // Only add UnknownPtr8Request if not using the legacy API.
                // Chat with SLxTnT - this is required for all request and needed before the main envelope.

                //if(customRequests.Any(x=>x.RequestType == RequestType.GetMapObjects  || x.RequestType == RequestType.GetPlayer))
                var plat8Message = new UnknownPtr8Request()
                {
                    Message = _client.UnknownPlat8Field
                };
                e.PlatformRequests.Add(new RequestEnvelope.Types.PlatformRequest()
                {
                    Type = PlatformRequestType.UnknownPtr8,
                    RequestMessage = plat8Message.ToByteString()
                });
            }
            e.PlatformRequests.Add(GenerateSignature(e, currentLocation));

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

        public double GenRandom(double num)
        {
            const float randomFactor = 0.3f;
            var randomMin = num * (1 - randomFactor);
            var randomMax = num * (1 + randomFactor);
            var randomizedDelay = RandomDevice.NextDouble() * (randomMax - randomMin) + randomMin;
            return randomizedDelay;
        }

        public double GenRandom(double min, double max)
        {
            return RandomDevice.NextDouble() * (max - min) + min;
        }
    }
}