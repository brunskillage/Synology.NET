using SynologyClient.Response;

namespace SynologyClient
{
    public class FavoriteEditResponse : BaseSynologyResponse
    {
        public FavoriteEdit Data { get; set; }
    }
}