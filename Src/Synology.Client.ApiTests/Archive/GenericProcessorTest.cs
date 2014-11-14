using System;
using FluentValidation;

namespace SynologyClient.ApiTests.Archive
{
    public class FunctionArgs
    {
        public string Name { get; set; }

        public string Sid { get; set; }

        public dynamic Args { get; set; }

        public Action Act { get; set; }

        public IValidator Validator { get; set; }

        public Func<dynamic, SynologyResponse> ErrorHandler { get; set; }
    }
}