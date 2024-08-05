using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.News
{
    public class SearchMethod
    {
        private readonly string _value;

        public SearchMethod(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }


        public static SearchMethod ByTitle { get { return new SearchMethod("TieuDe"); } }
        public static SearchMethod ByContent { get { return new SearchMethod("NoiDung"); } }
    }
}
