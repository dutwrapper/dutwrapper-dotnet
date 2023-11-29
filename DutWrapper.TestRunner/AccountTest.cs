using DutWrapper.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;

namespace DutWrapper.TestRunner
{
    [TestClass]
    public class AccountTest
    {
        public string SESSION_ID = null;

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
                throw new HttpRequestException("Failed while getting new Session ID! This test cannot continue.");

            Account.Login(SESSION_ID, data[0], data[1]).Wait();

            var result = Account.IsLoggedIn(SESSION_ID).Result;
            Console.WriteLine($"IsLoggedIn: {result} (Session ID: {SESSION_ID})");
            if (result != LoginStatus.LoggedIn)
                throw new HttpRequestException("Failed while logging you in! This test cannot continue.");

            var result3 = Account.GetSubjectScheduleList(SESSION_ID, 22, 1).Result;
            Console.WriteLine($"Subject schedule count: {(result3 == null ? "(unknown)" : result3.Count.ToString())} (Session ID: {SESSION_ID})");
            var result4 = Account.GetSubjectFeeList(SESSION_ID, 22, 1).Result;
            Console.WriteLine($"Subject fee count: {(result4 == null ? "(unknown)" : result4.Count.ToString())} (Session ID: {SESSION_ID})");
            var result5 = Account.GetAccountInformation(SESSION_ID).Result;
            Console.WriteLine($"Is account information null: {result5 == null} (Session ID: {SESSION_ID})");
            var result6 = Account.GetAccountTrainingResult(SESSION_ID).Result;
            Console.WriteLine($"Is account training result null: {result6 == null} (Session ID: {SESSION_ID})");

            Account.Logout(SESSION_ID).Wait();
            Console.WriteLine($"Logged out (Session ID: {SESSION_ID})");

            var result2 = Account.IsLoggedIn(SESSION_ID).Result;
            Console.WriteLine($"IsLoggedIn: {result2} (Session ID: {SESSION_ID})");
        }
    }
}
