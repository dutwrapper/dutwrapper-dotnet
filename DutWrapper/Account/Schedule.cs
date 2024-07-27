using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public static partial class Account
    {
        public class Schedule
        {
            /// <summary>
            /// 1: Sun, 2-7: Mon-Sat
            /// </summary>
            [JsonPropertyName("day_of_week")]
            public int DayOfWeek { get; private set; }

            [JsonPropertyName("lesson_affected")]
            public Range Lesson { get; private set; }

            [JsonPropertyName("room")]
            public string Room { get; private set; }

            public Schedule()
            {
                DayOfWeek = 1;
                Lesson = new Range(0, 0);
                Room = "";
            }

            public Schedule(int dayOfWeek, Range lesson, string room)
            {
                DayOfWeek = dayOfWeek;
                Lesson = lesson;
                Room = room;
            }
        }
    }
}
