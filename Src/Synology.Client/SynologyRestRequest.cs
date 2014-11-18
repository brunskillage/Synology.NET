using RestSharp;

namespace SynologyClient
{
    public class SynologyRestRequest : RestRequest
    {
        public SynologyRestRequest() : this(Method.GET) {}

        public SynologyRestRequest(Method method) : base(method)
        {
            OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
        }
    }
}
