﻿using SynologyClient.Response;

namespace SynologyClient
{
    public class SearchStopResponse : BaseSynologyResponse
    {
        public MethodStop Data { get; set; }
    }
}