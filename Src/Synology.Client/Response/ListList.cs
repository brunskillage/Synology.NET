using System.Collections.Generic;

namespace SynologyClient.Response
{
    public class ListList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public List<File> files { get; set; }
    }
}