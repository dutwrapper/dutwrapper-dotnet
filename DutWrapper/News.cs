using AngleSharp;
using DutWrapper.Model.Enums;
using DutWrapper.Model.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static partial class News
    {
        private static async Task<List<NewsGlobalItem>?> GetNews(NewsType? newsType = null, int page = 1, SearchMethod? searchType = null, string? searchQuery = null)
        {
            if (page < 1)
                throw new ArgumentException($"Page must be greater than 0! (current is {page})");

            List<NewsGlobalItem>? result = new List<NewsGlobalItem>();

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://sv.dut.udn.vn");

                HttpResponseMessage response = await client.GetAsync(string.Format(
                    @"/WebAjax/evLopHP_Load.aspx?E={0}&PAGETB={1}&COL={2}&NAME={3}&TAB=1",
                    newsType == null ? NewsType.Global.Value : newsType.Value,
                    page > 0 ? page : 1,
                    searchType == null ? SearchMethod.ByTitle.Value : searchType.Value,
                    searchQuery == null ? "" : searchQuery
                    ));
                if (!response.IsSuccessStatusCode)
                    throw new Exception(String.Format("The request has return code {0}.", response.StatusCode));

                var config = Configuration.Default;
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

                var htmlDocNews = document.GetElementsByClassName("tbBox").ToList();

                if (htmlDocNews == null || htmlDocNews.Count == 0)
                    throw new Exception($"No data from sv.dut.udn.vn in page {page}.");

                foreach (var htmlItem in htmlDocNews)
                {
                    NewsGlobalItem item = new NewsGlobalItem();

                    string title = htmlItem.GetElementsByClassName("tbBoxCaption")[0].TextContent;
                    string[] titleTemp = title.Split(new string[] { ":     " }, StringSplitOptions.None);

                    if (titleTemp.Length == 2)
                    {
                        item.Date = new DateTimeOffset(DateTime.ParseExact(titleTemp[0].Replace(" ", ""), "dd/MM/yyyy", CultureInfo.InvariantCulture), new TimeSpan(0,0,0)).ToUnixTimeMilliseconds();
                        item.Title = WebUtility.HtmlDecode(titleTemp[1]);
                        item.Content = htmlItem.GetElementsByClassName("tbBoxContent")[0].InnerHtml;
                        item.ContentString = htmlItem.GetElementsByClassName("tbBoxContent")[0].TextContent;
                    }
                    else
                    {
                        item.Title = WebUtility.HtmlDecode(title);
                        item.Content = htmlItem.GetElementsByClassName("tbBoxContent")[0].InnerHtml;
                        item.ContentString = htmlItem.GetElementsByClassName("tbBoxContent")[0].TextContent;
                    }

                    result.Add(item);
                }
            }
            catch
            {
                result.Clear();
                result = null;
            }

            return result;
        }

        public static async Task<List<NewsGlobalItem>?> GetNewsGlobal(int page = 1, SearchMethod? searchType = null, string? query = null)
        {
            return await GetNews(NewsType.Global, page, searchType, query);
        }

        public static async Task<List<NewsSubjectItem>?> GetNewsSubject(int page = 1, SearchMethod? searchType = null, string? query = null)
        {
            List<NewsGlobalItem>? newsCoreList = await GetNews(NewsType.Subject, page, searchType, query);
            if (newsCoreList == null) { return null; }

            List<NewsSubjectItem> newsSubjectList = new List<NewsSubjectItem>();
            Gender GetGender(string firstWord)
            {
                switch (firstWord.ToLower())
                {
                    case "thầy":
                        return Gender.Male;
                    case "cô":
                        return Gender.Female;
                    default:
                        return Gender.Unknown;
                }
            }
            foreach (var newsCoreItem in newsCoreList)
            {
                string? room = null;
                DateTime affectedDate = new DateTime();
                Range? lessonItem = null;
                SubjectStatus subjectStatus = SubjectStatus.Notify;
                Gender lecturerGender = Gender.Unknown;

                List<string> regex = new List<string> {
                    @"(.*) nhắn: Lớp HỌC BÙ ngày: (\d{2}[-|\/]\d{2}[-|\/]\d{4}),Tiết: (\d{1,2}-\d{1,2}). phòng: (.*)",
                    @"(.*) nhắn: Lớp NGHỈ HỌC \(Tiết:(\d{1,2}-\d{1,2})\) ngày: (\d{2}[-|\/]\d{2}[-|\/]\d{4})"
                };

                foreach (var item in regex)
                {
                    MatchCollection mc = Regex.Matches(newsCoreItem.Content, item, RegexOptions.Multiline);
                    if (mc.Count >= 1)
                    {
                        if (mc[0].Groups.Count == 5)
                        {
                            lecturerGender = GetGender(mc[0].Groups[1].Value);
                            affectedDate = DateTime.ParseExact(mc[0].Groups[2].Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            lessonItem = new Range(
                                Convert.ToInt32(mc[0].Groups[3].Value.Split("-")[0]),
                                Convert.ToInt32(mc[0].Groups[3].Value.Split("-")[1])
                                );
                            room = mc[0].Groups[4].Value;
                            subjectStatus = SubjectStatus.MakeUpLesson;
                            break;
                        }
                        else if (mc[0].Groups.Count == 4)
                        {
                            lecturerGender = GetGender(mc[0].Groups[1].Value);
                            lessonItem = new Range(
                                Convert.ToInt32(mc[0].Groups[2].Value.Split("-")[0]),
                                Convert.ToInt32(mc[0].Groups[2].Value.Split("-")[1])
                                );
                            affectedDate = DateTime.ParseExact(mc[0].Groups[3].Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            subjectStatus = SubjectStatus.Leaving;
                            break;
                        }
                        else { }
                    }
                }

                newsSubjectList.Add(new NewsSubjectItem()
                {
                    Date = newsCoreItem.Date,
                    Title = newsCoreItem.Title,
                    Content = newsCoreItem.Content,
                    ContentString = newsCoreItem.ContentString,
                    LecturerGender = GetGender(newsCoreItem.Title.Split(" thông báo đến lớp: ", 2)[0].Split(" ", 2)[0].ToLower()),
                    LecturerName = newsCoreItem.Title.Split(" thông báo đến lớp: ", 2)[0].Split(" ", 2)[1],
                    AffectedClass = newsCoreItem.Title.Split(" thông báo đến lớp: ", 2)[1].Split(" , ").ToList(),
                    Status = subjectStatus,
                    DateAffected = new DateTimeOffset(affectedDate, new TimeSpan(0,0,0)).ToUnixTimeMilliseconds(),
                    Room = room,
                });
            }

            // Make up: (.*) nhắn: Lớp HỌC BÙ ngày: (\d{2}[-|\/]\d{2}[-|\/]\d{4}),Tiết: (\d{1,2}-\d{1,2}). phòng: (.*)
            // Leaving: (.*) nhắn: Lớp NGHỈ HỌC \(Tiết:(\d{1,2}-\d{1,2})\) ngày: (\d{2}[-|\/]\d{2}[-|\/]\d{4})

            return newsSubjectList;
        }
    }
}
