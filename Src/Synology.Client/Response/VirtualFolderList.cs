namespace SynologyClient.Response
{
    public class VirtualFolderList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public VirtualFolder[] folders { get; set; }
    }
}