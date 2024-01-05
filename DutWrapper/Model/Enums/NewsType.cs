using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Model.Enums
{
    public class NewsType
    {
        private readonly string _value;

        public NewsType(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Global news.
        /// </summary>
        public static NewsType Global { get { return new NewsType("CTRTBSV"); } }
        /// <summary>
        /// Subject news.
        /// </summary>
        public static NewsType Subject { get { return new NewsType("CTRTBGV"); } }
    }
}
