using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public class SubjectCode
    {
        [JsonPropertyName("subjectid")]
        public string? SubjectId { get; private set; }

        [JsonPropertyName("schoolyearid")]
        public string? SchoolYear { get; private set; }

        [JsonPropertyName("studentyearid")]
        public string StudentYearId { get; private set; }

        [JsonPropertyName("classid")]
        public string ClassId { get; private set; }

        public SubjectCode(string studentYearId, string classId)
        {
            this.StudentYearId = studentYearId;
            this.ClassId = classId;
        }

        public SubjectCode(string? subjectId, string? schoolYear, string studentYearId, string classId)
        {
            this.SubjectId = subjectId;
            this.SchoolYear = schoolYear;
            this.StudentYearId = studentYearId;
            this.ClassId = classId;
        }

        public override string ToString()
        {
            return (SubjectId == null || SchoolYear == null)
                ? string.Format("{0}.{1}", StudentYearId, ClassId)
                : string.Format("{0}.{1}.{2}.{3}", SubjectId, SchoolYear, StudentYearId, ClassId);
        }
    }
}
