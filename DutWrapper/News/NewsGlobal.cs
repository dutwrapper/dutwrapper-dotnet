using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public static partial class News
    {
        public class NewsGlobal
        {
            /// <summary>
            /// News title.
            /// </summary>
            [JsonPropertyName("title")]
            public string? Title { get; set; } = null;

            /// <summary>
            /// News content in HTML.
            /// </summary>
            [JsonPropertyName("content")]
            public string? Content { get; set; } = null;

            /// <summary>
            /// News content in plain text.
            /// </summary>
            [JsonPropertyName("content_string")]
            public string? ContentString { get; set; } = null;

            /// <summary>
            /// News date when it posted.
            /// </summary>
            [JsonPropertyName("date")]
            public long Date { get; set; } = 0;

            /// <summary>
            /// Links in this news.
            /// </summary>
            [JsonPropertyName("links")]
            public List<NewsLink> Links { get; set; } = new List<NewsLink>();

            [JsonIgnore]
            public DateTimeOffset DateTime
            {
                get { return DateTimeOffset.FromUnixTimeMilliseconds(Date); }
            }

            public bool Equals(NewsGlobal news)
            {
                if (base.Equals(news))
                    return true;

                if (news.Title != Title ||
                    news.Content != Content ||
                    news.ContentString != ContentString ||
                    news.Date != Date
                    )
                    return false;

                return true;
            }

            public string ToJson()
            {
                return JsonSerializer.Serialize<NewsGlobal>(this, new JsonSerializerOptions
                {
                    IgnoreNullValues = false
                });
            }
        }
    }
}
