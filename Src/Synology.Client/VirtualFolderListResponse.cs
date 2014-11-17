using SynologyClient.Response;

namespace SynologyClient
{
    public class VirtualFolderListResponse : BaseSynologyResponse
    {
        public VirtualFolderList Data { get; set; }
    }
}