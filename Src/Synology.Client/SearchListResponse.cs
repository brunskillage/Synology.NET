using SynologyClient.Response;

namespace SynologyClient
{
    public class SearchListResponse : BaseSynologyResponse
    {
        public SearchList Data { get; set; }
    }
}