using System.Collections.Generic;

namespace SynologyClient
{
    public class SharingList
    {
        public int total { get; set; }

        public int offset { get; set; }

        public List<SharingLink> links { get; set; }
    }
}
