﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static partial class Utils
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
