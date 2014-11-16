namespace SynologyClient.Response
{
    public class ListList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public File[] files { get; set; }
    }
}