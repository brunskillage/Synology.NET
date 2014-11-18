using System.Collections.Generic;

namespace SynologyClient
{
    public interface ISynologyResponse
    {
        bool success { get; set; }

        dynamic data { get; set; }

        dynamic error { get; set; }

        int code { get; set; }

        List<object> errors { get; set; }
    }
}
