using AngleSharp;
using DutWrapper.Accounts;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutWrapper.Tester
{
    [TestClass]
    public class UtilsTest
    {
        public Formatting JSON_FORMATTING = Formatting.Indented;

        [TestMethod]
        public void GetCurrentSchoolWeek()
        {
            Debug.WriteLine($"Processing DutSchoolYearItem.GetCurrentSchoolYear...");
            var resultCurrentWeek = DutSchoolYear.GetCurrentSchoolYear().Result;
            var data = JsonConvert.SerializeObject(resultCurrentWeek, JSON_FORMATTING);
            Debug.WriteLine($"Result: {data}");
            Debug.WriteLine($"Result (ToString()): {resultCurrentWeek.ToString()}");
            Debug.WriteLine("");
        }

        [TestMethod]
        public async Task TestingNewWebParsingTools()
        {
            string htmlData = File.ReadAllText(@"D:\Documents\DUT - Subject Information and Subject Fee (2-2020-2021) (11_24_2024 11.48.30 PM).html");
            var htmlDoc = await WebParsingUtils.AngleSharpHtmlToDocument(htmlData);

            try
            {
                List<SubjectInformation> result = new List<SubjectInformation>();

                var htmlTableSchStudy = htmlDoc.GetElementById("TTKB_GridInfo");
                var tableSchStudy = WebParsingUtils.WebTable.ParseTableTag(htmlTableSchStudy);

                for (int i = 0; i < tableSchStudy.Rows.Count; i++)
                {
                    SubjectInformation item = new SubjectInformation
                    {
                        ID = tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Mã lớp học phần"))?.Value,
                        Name = tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Tên lớp học phần"))?.Value,
                        Credit = tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Số TC"))?.FloatValue ?? 0f,
                        IsHighQuality = tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("CLC"))?.BoolCellCheck ?? false,
                        Lecturer = tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Giảng viên"))?.Value,
                        ScheduleStudy = new ScheduleStudy(
                            scheduleList: tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Thời khóa biểu"))?.Value?.Split("; ").Select(item1 =>
                            {
                                return new Schedule(
                                            item1.Split(",")[0].StartsWith("Thứ ") ? Convert.ToInt32(item1.Split(",")[0].Remove(0, 4)) : 1,
                                            new Range(
                                                Convert.ToInt32(item1.Split(",")[1].Split("-")[0]),
                                                Convert.ToInt32(item1.Split(",")[1].Split("-")[1])
                                                ),
                                            item1.Split(",")[2]
                                            );
                            }).ToList() ?? new List<Schedule>(),
                            weekAffected: tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Tuần học"))?.Value?.Split(";").Select(item1 =>
                            {
                                return new Range(
                                        Convert.ToInt32(item1.Split("-")[0]),
                                        Convert.ToInt32(item1.Split("-")[1])
                                    );
                            }).ToList() ?? new List<Range>()
                            ),
                        PointFomula = tableSchStudy.GetValueFromHeader(i, tableSchStudy.GetFirstHeaderByName("Thông tin lớp học phần")?.GetHeaderByName("Công thức điểm"))?.Value,
                    };
                }

                var htmlTableSchExam = htmlDoc.GetElementById("TTKB_GridInfo");
                var tableSchExam = WebParsingUtils.WebTable.ParseTableTag(htmlTableSchExam);


                // TODO: Schedule Examination
                var docExam = htmlDoc.GetElementById("TTKB_GridLT");
                var rowListExam = docExam?.GetElementsByClassName("'GridRow").ToList();
                if (rowListExam != null && rowListExam.Count > 0)
                {
                    foreach (var row in rowListExam)
                    {
                        try
                        {
                            var cellCollection = row.GetElementsByClassName("GridCell");

                            var item = result.Where(p => p.ID == cellCollection[1].TextContent).First();
                            if (item == null)
                                continue;

                            ScheduleExam schEx = new ScheduleExam();
                            schEx.GroupExam = cellCollection[3].TextContent;
                            schEx.IsGlobalExam = cellCollection[4].ClassList.Contains("GridCheck");
                            schEx.DateExamInString = cellCollection[5].TextContent;

                            if (schEx.DateExamInString == null)
                                continue;

                            DateTime? dateTime = null;
                            string[] splited = schEx.DateExamInString.Split(new string[] { ", " }, StringSplitOptions.None);
                            string? time = null;
                            for (int i = 0; i < splited.Length; i++)
                            {
                                switch (splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[0])
                                {
                                    case "Phòng":
                                        schEx.RoomExam = splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[1];
                                        break;
                                    case "Ngày":
                                        dateTime = DateTime.ParseExact(splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        break;
                                    case "Giờ":
                                        time = splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[1];
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (dateTime != null && time != null)
                            {
                                dateTime = dateTime.Value.AddHours(Convert.ToInt32(time.Split('h')[0]));
                                if (time.Split('h').Length == 2)
                                {
                                    if (int.TryParse(time.Split('h')[1], out int minute))
                                        dateTime = dateTime.Value.AddMinutes(Convert.ToInt32(minute));
                                }
                                // -new DateTime(1970, 1, 1) for UnixTimeStamp.
                                // -7 because of GMT + 7.
                                schEx.DateExamInUnix = (long)dateTime.Value.Subtract(new DateTime(1970, 1, 1)).Add(new TimeSpan(-7, 0, 0)).TotalSeconds;
                            }

                            item.ScheduleExam = schEx;
                        }
                        catch
                        {
                            // TODO: Print error here!
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception when parsing subject schedule.
                throw ex;
            }
        }
    }
}
