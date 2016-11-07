#region using directives

using System;

#endregion

namespace PokemonGo.RocketAPI.Helpers
{
    public class DeviceInfo
    {
        public DeviceInfo()
        {
            if (DeviceId == null)
            {
                if (HardwareManufacturer == "Apple")
                    DeviceId = GenerateRandomDeviceId(20);
                else
                    DeviceId = GenerateRandomDeviceId();
            }
        }

        public string AndroidBoardName { get; set; }
        public string AndroidBootloader { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviceId { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceModelBoot { get; set; }
        public string DeviceModelIdentifier { get; set; }
        public string FirmwareBrand { get; set; }
        public string FirmwareFingerprint { get; set; }
        public string FirmwareTags { get; set; }
        public string FirmwareType { get; set; }
        public string HardwareManufacturer { get; set; }
        public string HardwareModel { get; set; }
        
        private static string GenerateRandomDeviceId(long numBytes = 16)
        {
            var bytes = new byte[numBytes];
            new Random().NextBytes(bytes);
            return DeviceInfoHelper.BytesToHex(bytes);
        }
    }
}