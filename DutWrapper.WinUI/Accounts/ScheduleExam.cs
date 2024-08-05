using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.WinUI.Accounts
{
    public class ScheduleExam
    {
        [JsonPropertyName("group")]
        public string? GroupExam { get; set; } = null;

        [JsonPropertyName("is_global")]
        public bool IsGlobalExam { get; set; } = false;

        [JsonPropertyName("room")]
        public string? RoomExam { get; set; } = null;

        [JsonPropertyName("date_string")]
        public string? DateExamInString { get; set; } = null;

        [JsonPropertyName("date")]
        public long DateExamInUnix { get; set; } = 0;

        public ScheduleExam() { }

        public ScheduleExam(string? groupExam, bool isGlobalExam, string? roomExam, string? dateExamInString, long dateExamInUnix)
        {
            GroupExam = groupExam;
            IsGlobalExam = isGlobalExam;
            RoomExam = roomExam;
            DateExamInString = dateExamInString;
            DateExamInUnix = dateExamInUnix;
        }
    }
}
