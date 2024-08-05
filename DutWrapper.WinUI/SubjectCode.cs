using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.WinUI
{
    public class SubjectCode
    {
        [JsonPropertyName("subject_id")]
        public string SubjectId { get; private set; }

        [JsonPropertyName("school_year_id")]
        public string SchoolYear { get; private set; }

        [JsonPropertyName("student_year_id")]
        public string StudentYearId { get; private set; }

        [JsonPropertyName("class_id")]
        public string ClassId { get; private set; }

        public SubjectCode(string studentYearId, string classId)
        {
            StudentYearId = studentYearId;
            ClassId = classId;
        }

        public SubjectCode(string subjectId, string schoolYear, string studentYearId, string classId)
        {
            SubjectId = subjectId;
            SchoolYear = schoolYear;
            StudentYearId = studentYearId;
            ClassId = classId;
        }

        public override string ToString()
        {
            return SubjectId == null || SchoolYear == null
                ? string.Format("{0}.{1}", StudentYearId, ClassId)
                : string.Format("{0}.{1}.{2}.{3}", SubjectId, SchoolYear, StudentYearId, ClassId);
        }
    }
}
