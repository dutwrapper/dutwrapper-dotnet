using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DutWrapper.WinUI.News
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
        [JsonPropertyName("content_html")]
        public string? ContentHTML { get; set; } = null;

        /// <summary>
        /// News content in plain text.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; } = null;

        /// <summary>
        /// News date when it posted.
        /// </summary>
        [JsonPropertyName("date")]
        public long Date { get; set; } = 0;

        /// <summary>
        /// Resources in this news.
        /// </summary>
        [JsonPropertyName("resources")]
        public List<NewsResource> Resources { get; set; } = new List<NewsResource>();

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
                news.ContentHTML != ContentHTML ||
                news.Content != Content ||
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
