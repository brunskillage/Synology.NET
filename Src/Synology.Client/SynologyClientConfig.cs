using System.Configuration;
using System.Runtime.InteropServices;

namespace SynologyClient
{
    public class SynologyClientConfig : ISynologyClientConfig
    {
        public SynologyClientConfig()
        {
            ApiBaseAddressAndPathNoTrailingSlash = ConfigurationManager.AppSettings.Get("Syno.ApiBaseAddress");
            User = ConfigurationManager.AppSettings.Get("Syno.User");
            Password = ConfigurationManager.AppSettings.Get("Syno.Pass");

            if(string.IsNullOrWhiteSpace(ApiBaseAddressAndPathNoTrailingSlash) || string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password))
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
