﻿using SynologyClient.Response;

namespace SynologyClient
{
    public class CompressStartResponse : BaseSynologyResponse
    {
        public MethodStop Data { get; set; }
    }
}