using DutWrapper.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace DutWrapper.Tester
{
    [TestClass]
    public class AccountTest
    {
        public string SESSION_ID = null;
        public int YEAR = 22;
        public int SEMESTER = 1; // 1,2, 3 is 2 in summer
        public Formatting JSON_FORMATTING = Formatting.Indented;

        [TestMethod]
        public void TestEntireAccountFunction()
        {
            string data_env = Environment.GetEnvironmentVariable("dut_account");
            if (data_env == null)
                throw new ArgumentException("dut_account environment variable not found. Please, add or modify this environment in format \"username|password\"");
            string[] data = data_env.Split("|");
            if (data.Length != 2)
                throw new ArgumentException("Something wrong with your dut_account environment variable. Please, add or modify this environment in format \"username|password\"");

            SESSION_ID = Account.GenerateNewSessionId().Result;
            if (SESSION_ID == null)
            {
                throw new HttpRequestException("Failed while getting new Session ID! This test cannot continue.");
            }
            else Debug.WriteLine($"Session ID: { SESSION_ID }");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.Login...");
            Account.Login(SESSION_ID, data[0], data[1]).Wait();
            Debug.WriteLine("Completed task.");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.IsLoggedIn...");
            var resultIsLoggedIn = Account.IsLoggedIn(SESSION_ID).Result;
            Debug.WriteLine($"Result: { JsonConvert.SerializeObject(resultIsLoggedIn, JSON_FORMATTING) }");
            if (resultIsLoggedIn != LoginStatus.LoggedIn)
            {
                throw new HttpRequestException("Failed while logging you in! This test cannot continue.");
            }
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetSubjectScheduleList...");
            var resultSubSchedule = Account.GetSubjectScheduleList(SESSION_ID, YEAR, SEMESTER).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultSubSchedule, JSON_FORMATTING)}");
            Debug.WriteLine($"Count: {(resultSubSchedule == null ? "(unknown)" : resultSubSchedule.Count.ToString())}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetSubjectFeeList...");
            var resultSubFee = Account.GetSubjectFeeList(SESSION_ID, YEAR, SEMESTER).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultSubFee, JSON_FORMATTING)}");
            Debug.WriteLine($"Count: {(resultSubFee == null ? "(unknown)" : resultSubFee.Count.ToString())}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetAccountInformation...");
            var resultAccInfo = Account.GetAccountInformation(SESSION_ID).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultAccInfo, JSON_FORMATTING)}");
            Debug.WriteLine($"Is account information null: {resultAccInfo == null}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetAccountTrainingResult...");
            var resultAccTrainStat = Account.GetAccountTrainingResult(SESSION_ID).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultAccTrainStat, JSON_FORMATTING)}");
            Debug.WriteLine($"Is account training result null: {resultAccTrainStat == null}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.Logout...");
            Account.Logout(SESSION_ID).Wait();
            Debug.WriteLine("Completed task.");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.IsLoggedIn...");
            var resultIsLoggedIn2 = Account.IsLoggedIn(SESSION_ID).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultIsLoggedIn2, JSON_FORMATTING)}");
            Debug.WriteLine($"IsLoggedIn: {resultIsLoggedIn2}");
            Debug.WriteLine("");
        }
    }
}
