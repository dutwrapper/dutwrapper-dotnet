using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DutWrapper.Tester
{
    [TestClass]
    public class AccountTest
    {
        public int YEAR = 20;
        public int SEMESTER = 2; // 1,2, 3 is 2 in summer
        public Formatting JSON_FORMATTING = Formatting.Indented;

        [TestMethod]
        public void TestEntireAccountFunction()
        {
            string? data_env = Environment.GetEnvironmentVariable("dut_account");
            if (data_env == null)
                throw new ArgumentException("dut_account environment variable not found. Please, add or modify this environment in format \"username|password\"");
            string[] data = data_env.Split("|");
            if (data.Length != 2)
                throw new ArgumentException("Something wrong with your dut_account environment variable. Please, add or modify this environment in format \"username|password\"");

            Account.Session session = Account.GenerateSessionAsync().Result;
            session.EnsureValidSession();
            session.EnsureValidViewState();
            Account.AuthInfo auth = new Account.AuthInfo(data[0], data[1]);
            auth.EnsureValidAuth();
            Account.SchoolYear schoolYear = new Account.SchoolYear(YEAR, SEMESTER);

            Debug.WriteLine($"Session ID: { session.SessionId }");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.Login...");
            Account.LoginAsync(session, auth).Wait();
            Debug.WriteLine("Completed task.");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.IsLoggedIn...");
            var resultIsLoggedIn = Account.IsLoggedInAsync(session).Result;
            Debug.WriteLine($"Result: { JsonConvert.SerializeObject(resultIsLoggedIn, JSON_FORMATTING) }");
            if (resultIsLoggedIn != Account.LoginStatus.LoggedIn)
            {
                throw new HttpRequestException("Failed while logging you in! This test cannot continue.");
            }
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetSubjectScheduleList...");
            var resultSubSchedule = Account.FetchSubjectScheduleAsync(session, schoolYear).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultSubSchedule, JSON_FORMATTING)}");
            Debug.WriteLine($"Count: {(resultSubSchedule == null ? "(unknown)" : resultSubSchedule.Count.ToString())}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetSubjectFeeList...");
            var resultSubFee = Account.FetchSubjectFeeAsync(session, schoolYear).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultSubFee, JSON_FORMATTING)}");
            Debug.WriteLine($"Count: {(resultSubFee == null ? "(unknown)" : resultSubFee.Count.ToString())}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetAccountInformation...");
            var resultAccInfo = Account.FetchAccountInformationAsync(session).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultAccInfo, JSON_FORMATTING)}");
            Debug.WriteLine($"Is account information null: {resultAccInfo == null}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.GetAccountTrainingResult...");
            var resultAccTrainStat = Account.FetchAccountTrainingResult(session).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultAccTrainStat, JSON_FORMATTING)}");
            Debug.WriteLine($"Is account training result null: {resultAccTrainStat == null}");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.Logout...");
            Account.LogoutAsync(session).Wait();
            Debug.WriteLine("Completed task.");
            Debug.WriteLine("");

            Debug.WriteLine($"Processing Account.IsLoggedIn...");
            var resultIsLoggedIn2 = Account.IsLoggedInAsync(session).Result;
            Debug.WriteLine($"Result: {JsonConvert.SerializeObject(resultIsLoggedIn2, JSON_FORMATTING)}");
            Debug.WriteLine($"IsLoggedIn: {resultIsLoggedIn2}");
            Debug.WriteLine("");
        }
    }
}
