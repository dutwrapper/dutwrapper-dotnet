using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.Accounts
{
    public class SubjectInformation
    {
        #region Basic Information
        /// <summary>
        /// Subject ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; } = null;
        /// <summary>
        /// Subject name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; } = null;

        /// <summary>
        /// Subject credit
        /// </summary>
        [JsonPropertyName("credit")]
        public float Credit { get; set; } = 0;
        #endregion

        #region Subject Information
        [JsonPropertyName("is_high_quality")]
        public bool IsHighQuality { get; set; } = false;

        [JsonPropertyName("lecturer")]
        public string? Lecturer { get; set; } = null;

        [JsonPropertyName("schedule_study")]
        public ScheduleStudy ScheduleStudy { get; set; } = new ScheduleStudy();

        [JsonPropertyName("point_formula")]
        public string? PointFomula { get; set; } = null;
        #endregion

        [JsonPropertyName("schedule_exam")]
        public ScheduleExam ScheduleExam { get; set; } = new ScheduleExam();

        public bool Equals(SubjectInformation sub)
        {
            if (base.Equals(sub))
                return true;

            // Basic information
            if (sub.ID != ID ||
                sub.Name != Name
                )
                return false;

            // Subject information
            if (sub.Credit != Credit ||
                sub.IsHighQuality != IsHighQuality ||
                sub.Lecturer != Lecturer ||
                // TODO: Need more check here!
                sub.ScheduleStudy != ScheduleStudy ||
                sub.PointFomula != PointFomula
                )
                return false;

            // Subject Examination Information
            // TODO: Need more check here!
            if (sub.ScheduleExam != ScheduleExam)
                return false;

            return true;
        }
    }
}
