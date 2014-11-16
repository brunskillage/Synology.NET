using SynologyClient.Response;

namespace SynologyClient
{
    public class ListGetInfoResponse : BaseSynologyResponse
    {
        public ListGetInfo Data { get; set; }
    }
}