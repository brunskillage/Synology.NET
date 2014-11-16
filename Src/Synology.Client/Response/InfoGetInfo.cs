namespace SynologyClient.Response
{
    public class InfoGetInfo
    {
        public bool is_manager { get; set; }
        public string support_virtual { get; set; }
        public bool support_vfs { get; set; }
        public bool support_sharing { get; set; }
        public string sharing { get; set; }
        public string hostname { get; set; }
    }
}