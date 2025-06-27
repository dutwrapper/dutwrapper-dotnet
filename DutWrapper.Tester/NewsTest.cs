using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using DutWrapper.News;

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
                var data = NewsInstance.GetNewsGlobal(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonSerializer.Serialize<List<NewsGlobal>>(news, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }

        [TestMethod]
        public void GetNews_Subject()
        {
            int NEWS_COUNT = 5;

            List<NewsSubject> news = new List<NewsSubject>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = NewsInstance.GetNewsSubject(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonSerializer.Serialize<List<NewsSubject>>(news, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }

        [TestMethod]
        public void GetNews_StudentAffairs()
        {
            int NEWS_COUNT = 5;

            List<NewsGlobal> news = new List<NewsGlobal>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = NewsInstance.GetNewsStudentAffairs(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonSerializer.Serialize<List<NewsGlobal>>(news, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }

        [TestMethod]
        public void GetNews_Examination()
        {
            int NEWS_COUNT = 5;

            List<NewsGlobal> news = new List<NewsGlobal>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = NewsInstance.GetNewsExamination(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonSerializer.Serialize<List<NewsGlobal>>(news, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }

        [TestMethod]
        public void GetNews_TuitionFee()
        {
            int NEWS_COUNT = 5;

            List<NewsGlobal> news = new List<NewsGlobal>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = NewsInstance.GetNewsTuitionFee(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonSerializer.Serialize<List<NewsGlobal>>(news, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }

        [TestMethod]
        public void GetNews_StatuteRegulation()
        {
            int NEWS_COUNT = 5;

            List<NewsGlobal> news = new List<NewsGlobal>();
            for (int i = 1; i <= NEWS_COUNT; i++)
            {
                var data = NewsInstance.GetNewsStatuteRegulation(i).Result;
                if (data == null)
                    throw new NullReferenceException($"Internal error from function. Did you connected the internet?");

                news.AddRange(data);
            }

            Debug.WriteLine($"Total news in {NEWS_COUNT} page(s): {news.Count}");
            Debug.WriteLine(JsonSerializer.Serialize<List<NewsGlobal>>(news, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
        }


    }
}
