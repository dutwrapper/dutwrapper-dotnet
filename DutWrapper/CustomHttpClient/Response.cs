using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.CustomHttpClient
{
    public class Response
    {
        public Uri Uri { get; private set; }

        public RequestType RequestType { get; private set; }

        public List<Header> Headers { get; private set; }

        public int? Code { get; private set; }

        public string? Content { get; private set; }

        public Exception? Exception { get; private set; }

        public Response(Uri uri, RequestType requestType = RequestType.Unknown, List<Header>? headers = null, int? code = null, string? content = null, Exception? ex = null)
        {
            Uri = uri;
            RequestType = requestType;
            Headers = headers ?? new List<Header>();
            Code = code;
            Content = content;
            Exception = ex;
        }

        public void EnsureNoException()
        {
            if (Exception != null)
            {
                throw Exception;
            }
        }

        public void EnsureSuccessfulRequest()
        {
            EnsureNoException();
            if (Code / 100 != 2 && Code / 100 != 3)
            {
                throw new Exception(string.Format("Server returned with code {0}.", Code));
            }
        }
    }
}
