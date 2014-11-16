namespace SynologyClient.Response
{
    public class Error
    {
        public int code { get; set; }
        public string path { get; set; }
        public Error[] errors { get; set; }
    }
}