﻿using SynologyClient.Response;

namespace SynologyClient
{
    public class SearchCleanResponse : BaseSynologyResponse
    {
        public MethodStop Data { get; set; }
    }
}