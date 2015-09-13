namespace SynologyClient
{
    public class GetBackgroundTasksResponse : BaseSynologyResponse
    {
        public BackgroundTaskList Data { get; set; }
    }
}