using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public static partial class News
    {
        public class NewsLink
        {
            [JsonPropertyName("text")]
            public string Text { get; private set; }

            [JsonPropertyName("url")]
            public string URL { get; private set; }

            [JsonPropertyName("position")]
            public int Position { get; private set; }

            public NewsLink(string text, string url, int position)
            {
                this.Text = text;
                this.URL = url;
                this.Position = position;
            }
        }
    }
}
