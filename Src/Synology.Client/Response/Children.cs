namespace SynologyClient.Response
{
    public class Children
    {
        public int total { get; set; }
        public int offset { get; set; }
        public File[] files { get; set; }
    }
}