using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Newtonsoft.Json;
using static DutWrapper.News;

namespace DutWrapper.Tester
{
    [TestClass]
    public class NewsTest
    {
        [TestMethod]
        public void GetNews_Global()
        {
            int NEWS_COUNT = 5;

            List<NewsGlobal> news = new List<NewsGlobal>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = News.GetNewsGlobal(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");
                if (data.Count == 0)
                    throw new NullReferenceException($"No datas in page {i}. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonConvert.SerializeObject(news, Formatting.Indented));
        }

        [TestMethod]
        public void GetNews_Subject()
        {
            int NEWS_COUNT = 5;

            List<NewsGlobal> news = new List<NewsGlobal>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = News.GetNewsSubject(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");
                if (data.Count == 0)
                    throw new NullReferenceException($"No datas in page {i}. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonConvert.SerializeObject(news, Formatting.Indented));
        }
    }
}
