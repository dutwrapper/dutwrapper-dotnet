using DutWrapper.CustomHttpClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DutWrapper
{
    public class DutSchoolYear
    {
        public DutSchoolYear(int week, int schoolYear, int dayOfWeek, DateTime firstDateOfSchoolYear)
        {
            Week = week;
            SchoolYear = schoolYear;
            FirstDateOfSchoolYear = firstDateOfSchoolYear;

            if (dayOfWeek < 1 || dayOfWeek > 7)
            {
                throw new Exception("DayOfWeek must be in range 1 (Sunday) - 7 (Saturday)!");
            }
            CurrentDayOfWeek = dayOfWeek;
        }

        /// <summary>
        /// Get current week in 1-52/53.
        /// </summary>
        [JsonPropertyName("week")]
        public int Week { get; private set; }

        /// <summary>
        /// Get current school year in short format (2-decimal)
        /// </summary>
        [JsonPropertyName("schoolYear")]
        public int SchoolYear { get; private set; }

        /// <summary>
        /// Get current day of week (1: Sunday, 2: Monday, ..., 7: Saturday)
        /// </summary>
        [JsonPropertyName("currentDayOfWeek")]
        public int CurrentDayOfWeek { get; private set; }

        /// <summary>
        /// Get first date of this school year.
        /// </summary>
        [JsonPropertyName("firstDateOfSchoolYear")]
        public DateTime FirstDateOfSchoolYear { get; private set; }

        public override string ToString()
        {
            return string.Format(
                "School year: 20{0}-20{1}, Week: {2}, Day of week: {3}, First date of school year: {4}",
                SchoolYear.ToString("00"),
                (SchoolYear + 1).ToString("00"),
                Week,
                CurrentDayOfWeek,
                FirstDateOfSchoolYear.ToString("dd/MM/yyyy")
                );
        }
    }
}
