﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public static partial class News
    {
        public class NewsResource
        {
            [JsonPropertyName("text")]
            public string Text { get; private set; }

            [JsonPropertyName("content")]
            public string Content { get; private set; }

            [JsonPropertyName("type")]
            public string Type { get; private set; }

            [JsonPropertyName("position")]
            public int Position { get; private set; }

            public NewsResource(string text, string type, string content, int position)
            {
                this.Text = text;
                this.Content = content;
                this.Type = type;
                this.Position = position;
            }
        }
    }
}