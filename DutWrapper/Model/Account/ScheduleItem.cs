using System;

namespace DutWrapper.Model.Account
{
    public class ScheduleItem
    {
        /// <summary>
        /// 1: Sun, 2-7: Mon-Sat
        /// </summary>
        int _dayOfWeek;

        Range _lesson;

        string _room;

        public ScheduleItem()
        {
            _dayOfWeek = 1;
            _lesson = new Range();
            _room = "";
        }

        public ScheduleItem(int dayOfWeek, Range lesson, string room)
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
