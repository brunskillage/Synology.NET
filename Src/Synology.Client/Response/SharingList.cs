using System.Collections.Generic;

namespace SynologyClient.Response
{
    public class SharingList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public List<SharingLink> links { get; set; }
    }
}