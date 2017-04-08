#region using directives

using System;
using System.Collections.Generic;
using Google.Protobuf;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Platform;
using POGOProtos.Networking.Platform.Requests;
using POGOProtos.Networking.Requests;
using POGOProtos.Enums;
using Troschuetz.Random;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Hash;
using Newtonsoft.Json;
using GeoCoordinatePortable;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class RequestBuilder
    {
        private const int GEOLOCATION_PRECISION = 5;

        private readonly TRandom TRandomDevice = new TRandom();
        private LehmerRng _lehmerRng = new LehmerRng();
        private readonly Client _client;
        private readonly ISettings _settings;
        private ByteString _sessionHash;
        private int _requestCount;
        private float _course;

        public RequestBuilder(Client client, ISettings settings)
        {
            _client = client;
            _settings = settings;

            if (_sessionHash == null)
                GenerateNewHash();
            
            _requestCount = 1;
            _course =  (float) TRandomDevice.NextDouble(0, 359.9);
        }
        
        public void GenerateNewHash()
        {
            var hashBytes = new byte[16];
            TRandomDevice.NextBytes(hashBytes);

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

        private async Task<RequestEnvelope.Types.PlatformRequest> GenerateSignature(RequestEnvelope requestEnvelope, GeoCoordinate currentLocation)
        {
            byte[] ticketBytes = requestEnvelope.AuthTicket != null ? requestEnvelope.AuthTicket.ToByteArray() : requestEnvelope.AuthInfo.ToByteArray();

            // Common device info
            var deviceInfo = new Signature.Types.DeviceInfo
            {
                DeviceId = _settings.DeviceId,
                DeviceBrand = _settings.DeviceBrand,
                DeviceModel = _settings.DeviceModel,
                DeviceModelBoot = _settings.DeviceModelBoot + "\0",
                HardwareManufacturer = _settings.HardwareManufacturer,
                HardwareModel = _settings.HardwareModel + "\0",
                FirmwareBrand = (_settings.DeviceModel == "iPhone" ? "iOS" : "iPhone OS"),
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

            if (sig.TimestampSinceStart < 5000)
                sig.TimestampSinceStart = (ulong)TRandomDevice.Next(5000, 8000);

            var sen = new Signature.Types.SensorInfo()
            {
                LinearAccelerationX = TRandomDevice.Triangular(-3, 1, 0),
                LinearAccelerationY = TRandomDevice.Triangular(-2, 3, 0),
                LinearAccelerationZ = TRandomDevice.Triangular(-4, 2, 0),
                MagneticFieldX = TRandomDevice.Triangular(-50, 50, 0),
                MagneticFieldY = TRandomDevice.Triangular(-60, 50, -5),
                MagneticFieldZ = TRandomDevice.Triangular(-60, 40, -30),
                MagneticFieldAccuracy = TRandomDevice.Choice(new List<int>(new int[] { -1, 1, 1, 2, 2, 2, 2 })),
                AttitudePitch = TRandomDevice.Triangular(-1.5, 1.5, 0.2),
                AttitudeYaw = TRandomDevice.NextDouble(-3, 3),
                AttitudeRoll = TRandomDevice.Triangular(-2.8, 2.5, 0.25),
                RotationRateX = TRandomDevice.Triangular(-6, 4, 0),
                RotationRateY = TRandomDevice.Triangular(-5.5, 5, 0),
                RotationRateZ = TRandomDevice.Triangular(-5, 3, 0),
                GravityX = TRandomDevice.Triangular(-1, 1, 0.15),
                GravityY = TRandomDevice.Triangular(-1, 1, -.2),
                GravityZ = TRandomDevice.Triangular(-1, .7, -0.8),
                Status = 3
            };
            sen.TimestampSnapshot = (ulong)TRandomDevice.NextUInt((uint)(sig.TimestampSinceStart - 5000), (uint)(sig.TimestampSinceStart - 100));
            sig.SensorInfo.Add(sen);

            var locationFix = new Signature.Types.LocationFix
            {
                Provider = TRandomDevice.Choice(new List<string>(new string[] { "network", "network", "network", "network", "fused" })),
                Latitude = (float)currentLocation.Latitude,
                Longitude = (float)currentLocation.Longitude,
                Altitude = (float)currentLocation.Altitude,
                ProviderStatus = 3,
                LocationType = 1
            };

            locationFix.TimestampSnapshot = (ulong)TRandomDevice.NextUInt((uint)(sig.TimestampSinceStart - 5000), (uint)(sig.TimestampSinceStart - 1000));
            
            if (requestEnvelope.Accuracy >= 65)
            {
                locationFix.HorizontalAccuracy = TRandomDevice.Choice(new List<float>(new float[] { (float)requestEnvelope.Accuracy, 65, 65, TRandomDevice.Next(66, 80), 200 }));
                if (_client.Platform == Platform.Ios)
                    locationFix.VerticalAccuracy = (float)TRandomDevice.Triangular(35, 100, 65);
            }
            else
            {
                locationFix.HorizontalAccuracy = (float)requestEnvelope.Accuracy;
                if (_client.Platform == Platform.Ios)
                {
                    locationFix.VerticalAccuracy = requestEnvelope.Accuracy > 10 ? (float)TRandomDevice.Choice(new List<double>(new double[] {
                        24,
                        32,
                        48,
                        48,
                        64,
                        64,
                        96,
                        128
                    })) : (float)TRandomDevice.Choice(new List<double>(new double[] {
                        3,
                        4,
                        6,
                        6,
                        8,
                        12,
                        24
                    }));
                }
            }
            
            locationFix.HorizontalAccuracy = (float)Math.Round(locationFix.HorizontalAccuracy, GEOLOCATION_PRECISION);
            locationFix.VerticalAccuracy = (float)Math.Round(locationFix.VerticalAccuracy, GEOLOCATION_PRECISION);

            if (_client.Platform == Platform.Ios)
            {
                sig.ActivityStatus = new Signature.Types.ActivityStatus()
                {
                    Stationary = true
                };
                sig.ActivityStatus.Tilting |= TRandomDevice.NextDouble() > 0.50;

                if (TRandomDevice.NextDouble() > 0.95)
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

            var hashRequest = new HashRequestContent()
            {
                Latitude64 = BitConverter.DoubleToInt64Bits(currentLocation.Latitude),
                Longitude64 = BitConverter.DoubleToInt64Bits(currentLocation.Longitude),
                Accuracy64 = BitConverter.DoubleToInt64Bits(requestEnvelope.Accuracy),
                AuthTicket = ticketBytes,
                SessionData = _sessionHash.ToByteArray(),
                Requests = new List<byte[]>(),
                Timestamp = sig.Timestamp
            };


            foreach (var request in requestEnvelope.Requests)
            {
                hashRequest.Requests.Add(request.ToByteArray());
            }

            var res = await _client.Hasher.RequestHashesAsync(hashRequest).ConfigureAwait(false);

            foreach (var item in res.RequestHashes)
            {
                sig.RequestHash.Add(((ulong) item));
            }
            sig.LocationHash1 = (int)res.LocationAuthHash;
            sig.LocationHash2 = (int)res.LocationHash;

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
            var accessToken = await _client.Login.GetValidAccessToken(true /* force refresh */).ConfigureAwait(false);

            requestEnvelope.AuthTicket = null;
            requestEnvelope.AuthInfo = new RequestEnvelope.Types.AuthInfo
            {
                Provider = accessToken.ProviderID,
                Token = new RequestEnvelope.Types.AuthInfo.Types.JWT
                {
                    Contents = accessToken.Token,
                    Unknown2 = (accessToken.ProviderID == "ptc") ? TRandomDevice.Choice(new List<int>(new int[] { 2, 8, 21, 21, 21, 28, 37, 56, 59, 59, 59 })) : 59
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
            requestEnvelope.PlatformRequests.Add(await GenerateSignature(requestEnvelope, currentLocation).ConfigureAwait(false));
        }

        public async Task<RequestEnvelope> GetRequestEnvelope(IEnumerable<Request> customRequests)
        {
            // Save the location
            var currentLocation = new GeoCoordinate(_client.CurrentLatitude, _client.CurrentLongitude, _client.CurrentAltitude);
            currentLocation.Latitude = Math.Round(currentLocation.Latitude, GEOLOCATION_PRECISION);
            currentLocation.Longitude = Math.Round(currentLocation.Longitude, GEOLOCATION_PRECISION);
            currentLocation.Altitude = Math.Round(currentLocation.Altitude, GEOLOCATION_PRECISION);

            var e = new RequestEnvelope
            {
                StatusCode = 2, //1
                RequestId = (ulong)GetNextRequestId(), //3
                Latitude = currentLocation.Latitude, //7
                Longitude = currentLocation.Longitude, //8
                Accuracy = TRandomDevice.Choice(new List<int>(new int[] { 5, 5, 5, 5, 10, 10, 10, 30, 30, 50, 65, TRandomDevice.Next(66, 80) })), //9
                MsSinceLastLocationfix = (long)TRandomDevice.Triangular(300, 30000, 10000) //12
            };

            e.Requests.AddRange(customRequests);
            
            if (_client.AuthTicket != null)
            {
                e.AuthTicket = _client.AuthTicket;
            }
            else
            {
                var accessToken = await _client.Login.GetValidAccessToken().ConfigureAwait(false);
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
            e.PlatformRequests.Add(await GenerateSignature(e, currentLocation).ConfigureAwait(false));

            return e;
        }

        public async Task<RequestEnvelope> GetRequestEnvelope(RequestType type, IMessage message)
        {
            return await GetRequestEnvelope(new Request[] { new Request
            {
                RequestType = type,
                RequestMessage = message.ToByteString()
            } }).ConfigureAwait(false);
        }

    }
}
