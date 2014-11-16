using System.Collections.Generic;

namespace SynologyClient.Response
{
    public class VirtualFolderList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public List<VirtualFolder> folders { get; set; }
    }
}