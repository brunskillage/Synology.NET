using RestSharp;
using System;
using System.Reflection;

namespace SynologyClient
{
    public class FuncProcessor
    {
        private static readonly SynologyClientConfig Config = new SynologyClientConfig();
        private readonly dynamic _args;
        private readonly dynamic _optionalArgs;
        private readonly string _scriptPath;
        private readonly string _sid;
        public SynoRestRequest RestRequest;

        public FuncProcessor(
            string scriptPath,
            string sid,
            dynamic args,
            dynamic optionalArgs = null)
        {
            
            if (string.IsNullOrWhiteSpace(scriptPath))
                throw new ArgumentNullException("scriptPath");
            if (string.IsNullOrWhiteSpace(sid))
                throw new ArgumentNullException("sid");
            if (args == null)
                throw new ArgumentNullException("args");

            _scriptPath = scriptPath;
            _sid = sid;
            _args = args;
            _optionalArgs = optionalArgs;
        }

        public SynologyResponse Run()
        {
            try
            {
                RestRequest = new SynoRestRequest();

                AddParametersFromObjectProperties(_args, RestRequest);

                if (_optionalArgs != null)
                    AddParametersFromObjectProperties(_optionalArgs, RestRequest);

                RestRequest.AddParameter("_sid", _sid);

                IRestClient client = new RestClient(Config.ApiBaseAddressAndPathNoTrailingSlash + _scriptPath);

                IRestResponse<SynologyResponse> response = client.Execute<SynologyResponse>(RestRequest);

                if (response.Data.success == false)
                {
                    throw new SynologyClientException(SynologyErrorCodes.Dic[(int)response.Data.error["code"]]);
                }

                return response.Data;
            }
            catch (Exception e)
            {
                throw new SynologyClientException(e.Message, e);
            }
        }

        private void AddParametersFromObjectProperties(object src, IRestRequest req)
        {
            PropertyInfo[] props = src.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo prop in props)
                req.AddParameter(prop.Name, prop.GetValue(src, null));
        }
    }
}