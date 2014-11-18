using RestSharp;
using System;
using SynologyClient.Response;

namespace SynologyClient
{
    public class SynologySession : ISynologySession
    {
        private readonly string _authBaseUrl;
        private readonly ISynologyClientConfig _config;

        public SynologySession(ISynologyClientConfig config)
        {
            _config = config;
            _authBaseUrl = string.Format("{0}/auth.cgi", _config.ApiBaseAddressAndPathNoTrailingSlash);
        }

        public DateTime loggedInTime { get; set; }

        public string sid { get; set; }

        public string taskId { get; set; }

        public ISynologySession Login()
        {
            var client = new RestClient(_authBaseUrl);
            var request = new SynoRestRequest();
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.AddParameter("api", "SYNO.API.Auth");
            request.AddParameter("version", "3");
            request.AddParameter("method", "login");
            request.AddParameter("account", _config.User);
            request.AddParameter("passwd", _config.Password);
            request.AddParameter("session", "FileStation");
            request.AddParameter("format", "cookie");

            IRestResponse<RawSynologyResponse> response = client.Execute<RawSynologyResponse>(request);
            if (response.Data.success)
            {
                sid = response.Data.data["sid"];
                loggedInTime = DateTime.UtcNow;
                return this;
            }
            throw new SynologyClientException("Login failure.");
        }

        public void LogOut()
        {
            var client = new RestClient(_authBaseUrl);
            var request = new SynoRestRequest();
            request.AddParameter("api", "SYNO.API.Auth");
            request.AddParameter("version", "1");
            request.AddParameter("method", "logout");
            request.AddParameter("session", "FileStation");
            client.Execute(request);
        }
    }
}