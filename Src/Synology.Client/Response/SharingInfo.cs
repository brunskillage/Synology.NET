namespace SynologyClient.Response
{
    public class SharingGetInfo
    {
        public string id { get; set; }

    }    
    
    public class SharingList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public SharingLink[] links { get; set; }
    }
}