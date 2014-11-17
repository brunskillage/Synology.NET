using SynologyClient.Response;

namespace SynologyClient
{
    public class FavoriteListResponse : BaseSynologyResponse
    {
        public FavoriteList Data { get; set; }
    }
}