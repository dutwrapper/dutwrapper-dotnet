using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.WinUI.CustomHttpClient
{
    public class Header
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public Header(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}={1};",
                Key,
                Value
                );
        }
    }
}
