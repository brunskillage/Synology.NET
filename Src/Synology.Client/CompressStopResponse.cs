using SynologyClient.Response;

namespace SynologyClient
{
    public class CompressStopResponse : BaseSynologyResponse
    {
        public MethodStart Data { get; set; }
    }
}