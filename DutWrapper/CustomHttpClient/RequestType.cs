using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static partial class CustomHttpClient
    {
        public enum RequestType
        {
            Unknown = -1,
            Get = 0,
            Post = 1
        }
    }
}
