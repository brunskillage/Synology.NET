namespace SynologyClient.Response
{
    public class SearchList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public bool finished { get; set; }
        public File[] files { get; set; }
    }
}