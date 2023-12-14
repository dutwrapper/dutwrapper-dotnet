using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static class NumberExtension
    {
        public static float SafeConvertToFloat(this string? s)
        {
            if (s == null)
            {
                return 0f;
            }

            float.TryParse(s, out float result);
            return result;
        }

        public static double SafeConvertToDouble(this string? s)
        {
            if (s == null)
            {
                return 0;
            }

            double.TryParse(s, out double result);
            return result;
        }

        public static int SafeConvertToInt(this string? s)
        {
            if (s == null)
            {
                return 0;
            }

            int.TryParse(s, out int result);
            return result;
        }
    }
}
