using System.Collections.Generic;

namespace SynologyClient.Response
{
    public class Error
    {
        public int code { get; set; }
        public string path { get; set; }
        public List<Error> errors { get; set; }
    }
}