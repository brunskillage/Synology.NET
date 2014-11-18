using System.Collections.Generic;

namespace SynologyClient
{
    public class BackgroundTask
    {
        public string api { get; set; }
        public string version { get; set; }
        public string method { get; set; }
        public string taskid { get; set; }
        public bool finished { get; set; }
        public dynamic @params { get; set; }
        public string path { get; set; }
        public int processed_num { get; set; }
        public int processed_size { get; set; }
        public string processing_path { get; set; }
        public int total { get; set; }
        public long progress { get; set; }
        public List<Error> errors { get; set; } 
    }
}