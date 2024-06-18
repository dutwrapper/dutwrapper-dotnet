using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static partial class CustomHttpClient
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
                this.Uri = uri;
                this.RequestType = requestType;
                this.Headers = headers ?? new List<Header>();
                this.Code = code;
                this.Content = content;
                this.Exception = ex;
            }

            public void EnsureNoException()
            {
                if (this.Exception != null)
                {
                    throw this.Exception;
                }
            }

            public void EnsureSuccessfulRequest()
            {
                EnsureNoException();
                if (this.Code / 100 != 2 && this.Code / 100 != 3)
                {
                    throw new Exception(string.Format("Server returned with code {0}.", this.Code));
                }
            }
        }
    }
}
