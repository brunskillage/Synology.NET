namespace SynologyClient.Response
{
    public class BackgroundTaskList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public BackgroundTask[] tasks { get; set; }
    }
}