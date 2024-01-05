using DutWrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Model.News
{
    public class NewsSubjectItem: NewsGlobalItem
    {
        public string? LecturerName { get; set; }

        public Gender LecturerGender { get; set; } = Gender.Unknown;

        public List<string> AffectedClass { get; set; } = new List<string>();

        public SubjectStatus Status { get; set; } = SubjectStatus.Unknown;

        public long? DateAffected { get; set; }

        public DateTimeOffset? DateTimeAffected
        {
            get { return DateAffected == null ? new DateTimeOffset?() : DateTimeOffset.FromUnixTimeMilliseconds((long)DateAffected); }
        }

        public string? Room { get; set; }

        public Range? LessonAffected { get; set; }
    }
}
