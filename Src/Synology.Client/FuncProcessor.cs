using System;
using System.Reflection;
using FluentValidation;
using RestSharp;

namespace SynologyClient
{
    public class FuncProcessor
    {
        private static readonly SynologyClientConfig Config = new SynologyClientConfig();
        private readonly dynamic _args;
        private readonly Func<dynamic, Exception, SynologyResponse> _errorHandler;
        private readonly dynamic _optionalArgs;
        private readonly string _scriptPath;
        private readonly string _sid;
        private readonly IValidator _validator;
        public SynoRestRequest RestRequest;

        public FuncProcessor(
            string scriptPath,
            string sid,
            dynamic args,
            dynamic optionalArgs = null,
            IValidator validator = null,
            Func<dynamic, Exception, SynologyResponse> errorHandler = null)
        {
            _scriptPath = scriptPath;
            if (scriptPath == null)
                throw new ArgumentNullException("scriptPath");
            if (sid == null)
                throw new ArgumentNullException("sid");
            if (args == null)
                throw new ArgumentNullException("args");
            _sid = sid;
            _args = args;
            _optionalArgs = optionalArgs;
            _validator = validator;
            _errorHandler = errorHandler;
        }

        public SynologyResponse Run()
        {
            try {
                if (_validator != null)
                    _validator.Validate(_args);

                RestRequest = new SynoRestRequest();

                AddParametersFromObjectProperties(_args, RestRequest);

                if (_optionalArgs != null)
                    AddParametersFromObjectProperties(_optionalArgs, RestRequest);

                RestRequest.AddParameter("_sid", _sid);

                var client = new RestClient(Config.ApiBaseAddressAndPathNoTrailingSlash + _scriptPath);
                IRestResponse<SynologyResponse> response = client.Execute<SynologyResponse>(RestRequest);

                if (response.Data.success == false) {
                   throw new SynologyClientException(SynologyErrorCodes.Dic[(int) response.Data.error["code"]]);
                }
                    

                return response.Data;
            }
            catch(Exception e) {
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