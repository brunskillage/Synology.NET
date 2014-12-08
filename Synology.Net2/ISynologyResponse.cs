using System.Collections.Generic;

namespace SynologyClient
{
    public interface ISynologyResponse
    {
        bool success { get; set; }

        object data { get; set; }

        object error { get; set; }

        int code { get; set; }

        List<object> errors { get; set; }
    }
}
