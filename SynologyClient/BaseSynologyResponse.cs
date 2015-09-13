using System.Collections.Generic;

namespace SynologyClient
{
    public class BaseSynologyResponse
    {
        // ReSharper disable InconsistentNaminge

        public bool success { get; set; }

        //public dynamic data { get; set; }

        public Error error { get; set; }

        public List<Error> errors { get; set; }

        public dynamic errormsg { get; set; }

        public int code { get; set; }

        public int http_status { get; set; }

        // ReSharper restore InconsistentNaming
    }

    //public class SynologyResponse2
    //{
    //    // ReSharper disable InconsistentNaminge
    //    public bool success { get; set; }

    //    public Error error { get; set; }

    //    public dynamic errormsg { get; set; }

    //    public int code { get; set; }

    //    public int http_status { get; set; }

    //    // ReSharper restore InconsistentNaming
    //}
}