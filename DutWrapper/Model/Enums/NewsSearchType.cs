using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Model.Enums
{
    public class NewsSearchType
    {
        private readonly string _value;

        public NewsSearchType(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

        public static NewsSearchType ByTitle { get { return new NewsSearchType("TieuDe"); } }
        public static NewsSearchType ByContent { get { return new NewsSearchType("NoiDung"); } }
    }
}
