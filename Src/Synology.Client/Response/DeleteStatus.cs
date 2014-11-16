namespace SynologyClient.Response
{
    public class DeleteStatus
    {
        public int processed_number { get; set; }
        public int total { get; set; }
        public string path { get; set; }
        public string processing_path { get; set; }
        public bool finished { get; set; }
        public long progress { get; set; }
    }
}