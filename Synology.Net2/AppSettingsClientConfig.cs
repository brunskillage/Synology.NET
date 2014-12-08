using System.Configuration;
using System.Runtime.InteropServices;

namespace SynologyClient
{
    public class AppSettingsClientConfig : ISynologyClientConfig
    {
        public AppSettingsClientConfig()
        {
            ApiBaseAddressAndPathNoTrailingSlash = ConfigurationSettings.AppSettings.Get("Syno.ApiBaseAddress");
            User = ConfigurationSettings.AppSettings.Get("Syno.User");
            Password = ConfigurationSettings.AppSettings.Get("Syno.Pass");

            if (string.IsNullOrEmpty(ApiBaseAddressAndPathNoTrailingSlash) || string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                string machineconfig = RuntimeEnvironment.GetRuntimeDirectory() + "Config\\machine.config";
                throw new SynologyClientException(
                    "Missing Credentials. Please enter the appSettings Syno.User, Syno.Pass, Syno.ApiBaseAddress keys and values in the app.config or machine config at " +
                    machineconfig);
            }
        }

        public string ApiBaseAddressAndPathNoTrailingSlash { get; private set; }

        public string User { get; private set; }

        public string Password { get; private set; }
    }
}
