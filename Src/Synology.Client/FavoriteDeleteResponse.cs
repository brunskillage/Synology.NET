using SynologyClient.Response;

namespace SynologyClient
{
    public class FavoriteDeleteResponse : BaseSynologyResponse
    {
        public FavoriteDelete Data { get; set; }
    }
}