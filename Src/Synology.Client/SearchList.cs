using System.Collections.Generic;

namespace SynologyClient
{
    public class SearchList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public bool finished { get; set; }
        public List<File> files { get; set; }
    }
}