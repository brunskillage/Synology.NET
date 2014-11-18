using System.Collections.Generic;

namespace SynologyClient
{
    public class ListList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public List<File> files { get; set; }
    }
}