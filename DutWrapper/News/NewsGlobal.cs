using System;

namespace DutWrapper
{
    public static partial class News
    {
        public class NewsGlobal
        {
            /// <summary>
            /// News title.
            /// </summary>
            public string? Title { get; set; } = null;

            /// <summary>
            /// News content in HTML.
            /// </summary>
            public string? Content { get; set; } = null;

            /// <summary>
            /// News content in plain text.
            /// </summary>
            public string? ContentString { get; set; } = null;

            /// <summary>
            /// News date when it posted.
            /// </summary>
            public long Date { get; set; } = 0;

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
        }
    }
}
