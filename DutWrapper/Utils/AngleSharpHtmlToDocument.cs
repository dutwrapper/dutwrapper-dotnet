using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static partial class Utils
    {
        public static async Task<IDocument> AngleSharpHtmlToDocument(string html)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            return await context.OpenAsync(req => req.Content(html));
        }
    }
}
