namespace SynologyClient.Response
{
    public class SharingLink
    {
        public string id { get; set; }
        public string url { get; set; }
        public string link_owner { get; set; }
        public string path { get; set; }
        public string isFolder { get; set; }
        public bool has_password { get; set; }
        public string date_expired { get; set; }
        public string date_available { get; set; }
        public string status { get; set; }
    }
}