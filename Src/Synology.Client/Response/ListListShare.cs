using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynologyClient.Response
{
    public class ListListShare
    {
        public int total { get; set; }
        public int offset { get; set; }
        public SharedFolder[] shares { get; set; }
    }
}
