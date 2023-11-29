using AngleSharp;
using DutWrapper.Model;
using DutWrapper.Model.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static partial class News
    {
        private static async Task<List<NewsGlobalItem>?> GetNews(NewsType newsType, int page = 1, string? query = null)
        {
            if (page < 1)
                throw new ArgumentException($"Page must be greater than 0! (current is {page})");

            List<NewsGlobalItem>? result = new List<NewsGlobalItem>();

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://sv.dut.udn.vn");

                HttpResponseMessage response = await client.GetAsync($"/WebAjax/evLopHP_Load.aspx?E={(newsType == NewsType.Global ? "CTRTBSV" : "CTRTBGV")}&PAGETB={(page > 0 ? page : 1)}&COL=TieuDe&NAME={query}&TAB=1");
                if (!response.IsSuccessStatusCode)
                    throw new Exception(String.Format("The request has return code {0}.", response.StatusCode));

                var config = Configuration.Default;
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

                var htmlDocNews = document.GetElementsByClassName("tbBox").ToList();

                // TODO: Add exception here.
                if (htmlDocNews == null || htmlDocNews.Count == 0)
                    throw new Exception($"No datas from sv.dut.udn.vn in page {page}.");

                foreach (var htmlItem in htmlDocNews)
                {
                    NewsGlobalItem item = new NewsGlobalItem();

                    string title = htmlItem.GetElementsByClassName("tbBoxCaption")[0].TextContent;
                    string[] titleTemp = title.Split(new string[] { ":&nbsp;&nbsp;&nbsp;&nbsp; " }, StringSplitOptions.None);

                    if (titleTemp.Length == 2)
                    {
                        item.Date = DateTime.ParseExact(titleTemp[0].Replace(" ", ""), "dd/MM/yyyy", CultureInfo.InvariantCulture);
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

        public static async Task<List<NewsGlobalItem>?> GetNewsGlobal(int page = 1, string? query = null)
        {
            return await GetNews(NewsType.Global, page, query);
        }

        public static async Task<List<NewsGlobalItem>?> GetNewsSubject(int page = 1, string? query = null)
        {
            return await GetNews(NewsType.Subject, page, query);
        }
    }
}
