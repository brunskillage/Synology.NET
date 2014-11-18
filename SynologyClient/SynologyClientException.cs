using System;
using System.Runtime.Serialization;

namespace SynologyClient
{
    [Serializable]
    public class SynologyClientException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public SynologyClientException() {}

        public SynologyClientException(string message) : base(message) {}

        public SynologyClientException(string message, Exception inner) : base(message, inner) {}

        protected SynologyClientException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}
