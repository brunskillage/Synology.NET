namespace SynologyClient
{
    public class SynologyResponse
    {
        // ReSharper disable InconsistentNaminge
        public bool success { get; set; }

        public dynamic data { get; set; }

        public dynamic error { get; set; }

        public dynamic errormsg { get; set; }

        public int code { get; set; }

        public int http_status { get; set; }

        // ReSharper restore InconsistentNaming
    }
}