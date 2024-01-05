using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DutWrapper
{
    public static partial class Utils
    {
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
    }
}
