using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.Accounts
{
    public class ScheduleStudy
    {
        [JsonPropertyName("schedule_list")]
        public List<Schedule> ScheduleList { get; private set; }

        [JsonPropertyName("week_affected")]
        public List<Range> WeekAffected { get; private set; }

        public ScheduleStudy()
        {
            ScheduleList = new List<Schedule>();
            WeekAffected = new List<Range>();
        }

        public ScheduleStudy(List<Schedule> scheduleList, List<Range> weekAffected)
        {
            ScheduleList = scheduleList;
            WeekAffected = weekAffected;
        }
    }
}
