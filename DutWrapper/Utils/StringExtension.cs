using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Utils
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string d)
        {
            return d.Length == 0;
        }

        public static bool IsNullOrEmpty(this string? d)
        {
            return d == null ? true : d.Length == 0;
        }
    }
}
