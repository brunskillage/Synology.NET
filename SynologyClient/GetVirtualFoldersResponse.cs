namespace SynologyClient
{
    public class GetVirtualFoldersResponse : BaseSynologyResponse
    {
        public VirtualFolderList Data { get; set; }
    }
}