using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static partial class Account
    {
        public class Schedule
        {
            /// <summary>
            /// 1: Sun, 2-7: Mon-Sat
            /// </summary>
            int _dayOfWeek;

            Range _lesson;

            string _room;

            public Schedule()
            {
                _dayOfWeek = 1;
                _lesson = new Range(0, 0);
                _room = "";
            }

            public Schedule(int dayOfWeek, Range lesson, string room)
            {
                _dayOfWeek = dayOfWeek;
                _lesson = lesson;
                _room = room;
            }

            public int DayOfWeek
            {
                get { return _dayOfWeek; }
            }

            public Range Lesson
            {
                get { return _lesson; }
            }

            public string Room
            {
                get { return _room; }
            }
        }
    }
}
