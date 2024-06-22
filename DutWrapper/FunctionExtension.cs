using AngleSharp.Dom;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static class FunctionExtension
    {
        public static bool IsEmpty(this string d)
        {
            return d.Length == 0;
        }

        public static bool IsNullOrEmpty(this string? d)
        {
            return d == null ? true : d.Length == 0;
        }

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

        #region AngleSharp extensions
        public static async Task<IDocument> AngleSharpHtmlToDocument(string html)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            return await context.OpenAsync(req => req.Content(html));
        }

        public static string? GetValue(this IElement? element)
        {
            return element == null ? null : element.GetAttribute("value");
        }

        public static string? GetTextContent(this IElement? element)
        {
            return element == null ? null : element.TextContent;
        }

        public static DateTime ConvertToDateTime(this IElement? element)
        {
            var dateText = element.GetValue();
            return dateText == null ? new DateTime() : DateTime.ParseExact(dateText, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static IElement? GetSelectedOptionOnSelectTag(this IElement? element)
        {
            return element == null ? null : element.GetElementsByTagName("option").ToList().FirstOrDefault(p => p.HasAttribute("selected"));
        }

        public static bool IsSelectedInInput(this IElement? element)
        {
            return element == null ? false : element.HasAttribute("checked");
        }

        public static IDocument? ConvertToIDocument(this IElement? element)
        {
            if (element == null)
                return null;

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(req => req.Content(element.InnerHtml)).Result;
            return document;
        }


        // External ==============================
        public static bool IsGridChecked(this IElement? element)
        {
            return element == null ? false : element.ClassList.Contains("GridCheck");
        }
        #endregion
    }
}
