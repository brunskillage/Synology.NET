using System.Collections.Generic;

namespace SynologyClient
{
    public class BackgroundTaskList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public List<BackgroundTask> tasks { get; set; }
    }
}