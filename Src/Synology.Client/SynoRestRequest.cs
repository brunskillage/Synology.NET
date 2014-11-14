using RestSharp;

namespace SynologyClient
{
    public class SynoRestRequest : RestRequest
    {
        public SynoRestRequest()
            : this(Method.GET)
        {
        }

        public SynoRestRequest(Method method)
            : base(method)
        {
            OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
        }
    }
}