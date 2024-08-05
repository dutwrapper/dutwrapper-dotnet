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
        [JsonPropertyName("schoolyear")]
        public int SchoolYear { get; private set; }

        /// <summary>
        /// Get current day of week (1: Sunday, 2: Monday, ..., 7: Saturday)
        /// </summary>
        [JsonPropertyName("currentdayofweek")]
        public int CurrentDayOfWeek { get; private set; }

        /// <summary>
        /// Get first date of this school year.
        /// </summary>
        [JsonPropertyName("firstdateofschoolyear")]
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

        public static async Task<DutSchoolYear> GetCurrentSchoolYear()
        {
            var response = await CustomHttpClientInstance.Get(new Uri(Variables.ServerUrl.DUT_LICHTUANURL));
            response.EnsureSuccessfulRequest();

            var document = await FunctionExtension.AngleSharpHtmlToDocument(response.Content!);

            var i1 = document.GetElementById("dnn_ctr442_View_cboNamhoc").GetSelectedOptionOnSelectTag();
            if (i1 == null)
            {
                // TODO: Throw here!
                throw new Exception();
            }

            // Get year string (but not need anymore)
            //var year = i1.GetTextContent();
            var yearValue = i1.GetValue();

            var i2 = document.GetElementById("dnn_ctr442_View_cboTuan").GetSelectedOptionOnSelectTag();
            if (i2 == null)
            {
                // TODO: Throw here!
                throw new Exception();
            }
            MatchCollection mc = Regex.Matches(
                i2.GetTextContent(),
                @"Tuần thứ (\d{1,2}): (\d{1,2}\/\d{1,2}\/\d{4})",
                RegexOptions.Multiline
                );
            if (mc.Count < 1)
            {
                // TODO: Throw here!
                throw new Exception();
            }
            var week = mc[0].Groups[1].Value.SafeConvertToInt();

            // Time from Vietnam
            var firstDateOfSchoolYear = DateTime.UtcNow.AddHours(7).Date;
            // Get first day from dayofweek (monday for vietnam)
            firstDateOfSchoolYear = firstDateOfSchoolYear.AddDays(
                firstDateOfSchoolYear.DayOfWeek == DayOfWeek.Sunday
                    ? -6
                    : -((int)firstDateOfSchoolYear.DayOfWeek - 1)
                );
            // Get first day from week in week var above.
            firstDateOfSchoolYear = firstDateOfSchoolYear.AddDays(-(7 * (week - 1)));

            return new DutSchoolYear(
                week: week,
                schoolYear: yearValue.SafeConvertToInt(),
                dayOfWeek: (int)DateTime.UtcNow.AddHours(7).DayOfWeek + 1,
                firstDateOfSchoolYear: firstDateOfSchoolYear
                );
        }
    }
}
