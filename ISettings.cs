#region using directives

using PokemonGo.RocketAPI.Enums;

#endregion

namespace PokemonGo.RocketAPI
{
    public interface ISettings
    {
        AuthType AuthType { get; set; }
        double DefaultLatitude { get; set; }
        double DefaultLongitude { get; set; }
        double DefaultAltitude { get; set; }
        string GoogleRefreshToken { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        string DevicePlatform { get; set; }
        string DeviceId { get; set; }
        string AndroidBoardName { get; set; }
        string AndroidBootloader { get; set; }
        string DeviceBrand { get; set; }
        string DeviceModel { get; set; }
        string DeviceModelIdentifier { get; set; }
        string DeviceModelBoot { get; set; }
        string HardwareManufacturer { get; set; }
        string HardwareModel { get; set; }
        string FirmwareBrand { get; set; }
        string FirmwareTags { get; set; }
        string FirmwareType { get; set; }
        string FirmwareFingerprint { get; set; }
        bool UseProxy { get; set; }
        bool UseProxyAuthentication { get; set; }
        string UseProxyHost { get; set; }
        string UseProxyPort { get; set; }
        string UseProxyUsername { get; set; }
        string UseProxyPassword { get; set; }
        bool UsePogoDevHashServer { get; set; }
        string UrlHashServices { get; set; }
        string EndPoint { get; set; }
        bool UseCustomAPI { get; set; }
        string AuthAPIKey { get; set; }
        bool DisplayVerboseLog { get; set; }
        bool AutoExitBotIfAccountFlagged { get; set; }
        double AccountLatitude { get; set; }
        double AccountLongitude { get; set; }
        bool AccountActive { get; set; }
        string Country { get; set; }
        string Language { get; set; }
        string TimeZone { get; set; }
        string POSIX { get; set; }
        double RunStart { get; set; }
        double RunEnd { get; set; }
    }
}
