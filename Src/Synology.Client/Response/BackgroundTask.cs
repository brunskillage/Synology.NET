namespace SynologyClient.Response
{
    public class BackgroundTask
    {
        public string api { get; set; }
        public string version { get; set; }
        public string crtime { get; set; }
        public string method { get; set; }
        public string taskid { get; set; }
        public bool finished { get; set; }
        public Params[] @params { get; set; }
        public string path { get; set; }
        public int processed_number { get; set; }
        public int processed_size { get; set; }
        public string processing_path { get; set; }
        public int total { get; set; }
        public long progress { get; set; }
    }
}