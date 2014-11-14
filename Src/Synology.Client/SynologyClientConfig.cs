using System.Configuration;
using System.Runtime.InteropServices;

namespace SynologyClient
{
    public class SynologyClientConfig : ISynologyClientConfig
    {
        public SynologyClientConfig()
        {
            ApiBaseAddressAndPathNoTrailingSlash = ConfigurationSettings.AppSettings.Get("Syno.ApiBaseAddress");
            User = ConfigurationSettings.AppSettings.Get("Syno.User");
            Password = ConfigurationSettings.AppSettings.Get("Syno.Pass");
            if (string.IsNullOrWhiteSpace(Password))
            {
                string machineconfig = RuntimeEnvironment.GetRuntimeDirectory() +
                                       "Config\\machine.config";
                throw new SynologyClientException(
                    "Missing Credentials. Please enter appsettings Syno.User, Syno.Pass, Syno.ApiBaseAddress in " +
                    machineconfig);
            }
        }

        public string ApiBaseAddressAndPathNoTrailingSlash { get; private set; }

        public string User { get; private set; }

        public string Password { get; private set; }
    }
}