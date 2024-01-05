using AngleSharp;
using DutWrapper.Model.Account;
using DutWrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static class Account
    {
        private static string BASE_ADDRESS = "http://sv.dut.udn.vn";
        private static string __VIEWSTATE = "/wEPDwUKMTY2NjQ1OTEyNA8WAh4TVmFsaWRhdGVSZXF1ZXN0TW9kZQIBFgJmD2QWAgIFDxYCHglpbm5lcmh0bWwF4i08dWwgaWQ9J21lbnUnIHN0eWxlPSd3aWR0aDogMTI4MHB4OyBtYXJnaW46IDAgYXV0bzsgJz48bGk+PGEgSUQ9ICdsUGFIT01FJyBzdHlsZSA9J3dpZHRoOjY1cHgnIGhyZWY9J0RlZmF1bHQuYXNweCc+VHJhbmcgY2jhu6c8L2E+PGxpPjxhIElEPSAnbFBhQ1REVCcgc3R5bGUgPSd3aWR0aDo4NXB4JyBocmVmPScnPkNoxrDGoW5nIHRyw6xuaDwvYT48dWwgY2xhc3M9J3N1Ym1lbnUnPjxsaT48YSBJRCA9J2xDb0NURFRDMicgICBzdHlsZSA9J3dpZHRoOjE0MHB4JyBocmVmPSdHX0xpc3RDVERULmFzcHgnPkNoxrDGoW5nIHRyw6xuaCDEkcOgbyB04bqhbzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NURFRDMScgICBzdHlsZSA9J3dpZHRoOjE0MHB4JyBocmVmPSdHX0xpc3RIb2NQaGFuLmFzcHgnPkjhu41jIHBo4bqnbjwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NURFRDMycgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdHX0xpc3RDVERUQW5oLmFzcHgnPlByb2dyYW08L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIElEPSAnbFBhS0hEVCcgc3R5bGUgPSd3aWR0aDo2MHB4JyBocmVmPScnPkvhur8gaG/huqFjaDwvYT48dWwgY2xhc3M9J3N1Ym1lbnUnPjxsaT48YSBJRCA9J2xDb0tIRFRDMScgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdodHRwczovLzFkcnYubXMvYi9zIUF0d0tsRFo2VnFidG5RY2JqUVFwS05rbUswX2g/ZT1uQ2I3eVAnPkvhur8gaG/huqFjaCDEkcOgbyB04bqhbyBuxINtIGjhu41jPC9hPjwvbGk+PGxpPjxhIElEID0nbENvS0hEVEMyJyAgIHN0eWxlID0nd2lkdGg6MjAwcHgnIGhyZWY9J2h0dHA6Ly9kazQuZHV0LnVkbi52bic+xJDEg25nIGvDvSBo4buNYzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0tIRFRDMycgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdodHRwOi8vZGs0LmR1dC51ZG4udm4vR19Mb3BIb2NQaGFuLmFzcHgnPkzhu5twIGjhu41jIHBo4bqnbiAtIMSRYW5nIMSRxINuZyBrw708L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzQnICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0nR19Mb3BIb2NQaGFuLmFzcHgnPkzhu5twIGjhu41jIHBo4bqnbiAtIGNow61uaCB0aOG7qWM8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzUnICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0naHR0cDovL2RrNC5kdXQudWRuLnZuL0dfREt5Tmh1Q2F1LmFzcHgnPkto4bqjbyBzw6F0IG5odSBj4bqndSBt4bufIHRow6ptIGzhu5twPC9hPjwvbGk+PGxpPjxhIElEID0nbENvS0hEVEM2JyAgIHN0eWxlID0nd2lkdGg6MjAwcHgnIGhyZWY9J2h0dHA6Ly9jYi5kdXQudWRuLnZuL1BhZ2VMaWNoVGhpS0guYXNweCc+VGhpIGN14buRaSBr4buzIGzhu5twIGjhu41jIHBo4bqnbjwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0tIRFRDNycgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdHX0RLVGhpTk4uYXNweCc+VGhpIFRp4bq/bmcgQW5oIMSR4buLbmgga+G7sywgxJHhuqd1IHJhPC9hPjwvbGk+PGxpPjxhIElEID0nbENvS0hEVEM4JyAgIHN0eWxlID0nd2lkdGg6MjAwcHgnIGhyZWY9J0dfTGlzdExpY2hTSC5hc3B4Jz5TaW5oIGhv4bqhdCBs4bubcCDEkeG7i25oIGvhu7M8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzknICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0naHR0cDovL2ZiLmR1dC51ZG4udm4nPkto4bqjbyBzw6F0IMO9IGtp4bq/biBzaW5oIHZpw6puPC9hPjwvbGk+PGxpPjxhIElEID0nbENvS0hEVEM5JyAgIHN0eWxlID0nd2lkdGg6MjAwcHgnIGhyZWY9J0dfREtQVkNELmFzcHgnPkhv4bqhdCDEkeG7mW5nIHBo4bulYyB24bulIGPhu5luZyDEkeG7k25nPC9hPjwvbGk+PC91bD48L2xpPjxsaT48YSBJRD0gJ2xQYVRSQUNVVScgc3R5bGUgPSd3aWR0aDo3MHB4JyBocmVmPScnPkRhbmggc8OhY2g8L2E+PHVsIGNsYXNzPSdzdWJtZW51Jz48bGk+PGEgSUQgPSdsQ29UUkFDVVUwMScgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3ROZ3VuZ0hvYy5hc3B4Jz5TaW5oIHZpw6puIG5n4burbmcgaOG7jWM8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUwMycgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RMb3AuYXNweCc+U2luaCB2acOqbiDEkWFuZyBo4buNYyAtIGzhu5twPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMDQnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0Q0NDTlRULmFzcHgnPlNpbmggdmnDqm4gY8OzIGNo4bupbmcgY2jhu4kgQ05UVDwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTA1JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdENDTk4uYXNweCc+U2luaCB2acOqbiBjw7MgY2jhu6luZyBjaOG7iSBuZ2/huqFpIG5n4buvPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMDYnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0naHR0cDovL2Rhb3Rhby5kdXQudWRuLnZuL1NWL0dfS1F1YUFuaFZhbi5hc3B4Jz5TaW5oIHZpw6puIHRoaSBUaeG6v25nIEFuaCDEkeG7i25oIGvhu7M8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUwNycgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3REb0FuVE4uYXNweCc+U2luaCB2acOqbiBsw6BtIMSQ4buTIMOhbiB04buRdCBuZ2hp4buHcDwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTA4JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdEhvYW5Ib2NQaGkuYXNweCc+U2luaCB2acOqbiDEkcaw4bujYyBob8OjbiDEkcOzbmcgaOG7jWMgcGjDrTwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTE2JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdEhvYW5fVGhpQlMuYXNweCc+U2luaCB2acOqbiDEkcaw4bujYyBob8OjbiB0aGksIHRoaSBi4buVIHN1bmc8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUwOScgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RIb2NMYWkuYXNweCc+U2luaCB2acOqbiBk4buxIHR1eeG7g24gdsOgbyBo4buNYyBs4bqhaTwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTEwJyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdEt5THVhdC5hc3B4Jz5TaW5oIHZpw6puIGLhu4sga+G7tyBsdeG6rXQ8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUxMScgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RCaUh1eUhQLmFzcHgnPlNpbmggdmnDqm4gYuG7iyBo4buneSBo4buNYyBwaOG6p248L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUxMicgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RMb2NrV2ViLmFzcHgnPlNpbmggdmnDqm4gYuG7iyBraMOzYSB3ZWJzaXRlPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMTMnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0TG9ja1dlYlRhbS5hc3B4Jz5TaW5oIHZpw6puIGLhu4sgdOG6oW0ga2jDs2Egd2Vic2l0ZTwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTE0JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdEhhbkNoZVRDLmFzcHgnPlNpbmggdmnDqm4gYuG7iyBo4bqhbiBjaOG6vyB0w61uIGNo4buJIMSRxINuZyBrw708L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUxNScgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RDYW5oQmFvS1FIVC5hc3B4Jz5TaW5oIHZpw6puIGLhu4sgY+G6o25oIGLDoW8ga+G6v3QgcXXhuqMgaOG7jWMgdOG6rXA8L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIElEPSAnbFBhQ1VVU1YnIHN0eWxlID0nd2lkdGg6ODhweCcgaHJlZj0nJz5D4buxdSBzaW5oIHZpw6puPC9hPjx1bCBjbGFzcz0nc3VibWVudSc+PGxpPjxhIElEID0nbENvQ1VVU1YxJyAgIHN0eWxlID0nd2lkdGg6MTEwcHgnIGhyZWY9J0dfTGlzdFROZ2hpZXAuYXNweCc+xJDDoyB04buRdCBuZ2hp4buHcDwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NVVVNWMicgICBzdHlsZSA9J3dpZHRoOjExMHB4JyBocmVmPSdHX0xpc3RLaG9uZ1ROLmFzcHgnPktow7RuZyB04buRdCBuZ2hp4buHcDwvYT48L2xpPjwvdWw+PC9saT48bGk+PGEgSUQ9ICdsUGFDU1ZDJyBzdHlsZSA9J3dpZHRoOjE0NXB4JyBocmVmPScnPlBow7JuZyBo4buNYyAmIEjhu4cgdGjhu5FuZzwvYT48dWwgY2xhc3M9J3N1Ym1lbnUnPjxsaT48YSBJRCA9J2xDb0NTVkMwMScgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdodHRwOi8vY2IuZHV0LnVkbi52bi9QYWdlQ05QaG9uZ0hvYy5hc3B4Jz5Uw6xuaCBow6xuaCBz4butIGThu6VuZyBwaMOybmcgaOG7jWM8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29DU1ZDMDInICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0VGhCaUhvbmcuYXNweCc+VGjhu5FuZyBrw6ogYsOhbyB0aGnhur90IGLhu4sgcGjDsm5nIGjhu41jIGjhu49uZzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NTVkMwOScgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX1N5c1N0YXR1cy5hc3B4Jz5UcuG6oW5nIHRow6FpIGjhu4cgdGjhu5FuZyB0aMO0bmcgdGluIHNpbmggdmnDqm48L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIElEPSAnbFBhTElFTktFVCcgc3R5bGUgPSd3aWR0aDo1MHB4JyBocmVmPScnPkxpw6puIGvhur90PC9hPjx1bCBjbGFzcz0nc3VibWVudSc+PGxpPjxhIElEID0nbENvTElFTktFVDEnICAgc3R5bGUgPSd3aWR0aDo3MHB4JyBocmVmPSdodHRwOi8vbGliLmR1dC51ZG4udm4nPlRoxrAgdmnhu4duPC9hPjwvbGk+PGxpPjxhIElEID0nbENvTElFTktFVDInICAgc3R5bGUgPSd3aWR0aDo3MHB4JyBocmVmPSdodHRwOi8vbG1zMS5kdXQudWRuLnZuJz5EVVQtTE1TPC9hPjwvbGk+PC91bD48L2xpPjxsaT48YSBJRD0gJ2xQYUhFTFAnIHN0eWxlID0nd2lkdGg6NDVweCcgaHJlZj0nJz5I4buXIHRy4bujPC9hPjx1bCBjbGFzcz0nc3VibWVudSc+PGxpPjxhIElEID0nbENvSEVMUDEnICAgc3R5bGUgPSd3aWR0aDoyMTBweCcgaHJlZj0naHR0cDovL2ZyLmR1dC51ZG4udm4nPkPhu5VuZyBo4buXIHRy4bujIHRow7RuZyB0aW4gdHLhu7FjIHR1eeG6v248L2E+PC9saT48bGk+PGEgSUQgPSdsQ29IRUxQMicgICBzdHlsZSA9J3dpZHRoOjIxMHB4JyBocmVmPSdodHRwczovL2RyaXZlLmdvb2dsZS5jb20vZmlsZS9kLzFaMHFsYmhLYVNHbXpFWkpEMnVCNGVVV2VlSGFROUhIbC92aWV3Jz5IxrDhu5tuZyBk4bqrbiDEkMSDbmcga8O9IGjhu41jPC9hPjwvbGk+PGxpPjxhIElEID0nbENvSEVMUDMnICAgc3R5bGUgPSd3aWR0aDoyMTBweCcgaHJlZj0naHR0cDovL2Rhb3Rhby5kdXQudWRuLnZuL2Rvd25sb2FkMi9FbWFpbF9HdWlkZS5wZGYnPkjGsOG7m25nIGThuqtuIFPhu60gZOG7pW5nIEVtYWlsIERVVDwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0hFTFA3JyAgIHN0eWxlID0nd2lkdGg6MjEwcHgnIGhyZWY9J2h0dHBzOi8vMWRydi5tcy91L3MhQXR3S2xEWjZWcWJ0bzEwYmhIYzBLN3NleU5Hcj9lYUNUYjh4Jz5WxINuIGLhuqNuIFF1eSDEkeG7i25oIGPhu6dhIFRyxrDhu51uZzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0hFTFA4JyAgIHN0eWxlID0nd2lkdGg6MjEwcHgnIGhyZWY9J2h0dHBzOi8vdGlueXVybC5jb20veTRrZGozc3AnPkJp4buDdSBt4bqrdSB0aMaw4budbmcgZMO5bmc8L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIGlkID0nbGlua0RhbmdOaGFwJyBocmVmPSdQYWdlRGFuZ05oYXAuYXNweCcgc3R5bGUgPSd3aWR0aDo4MHB4Oyc+IMSQxINuZyBuaOG6rXAgPC9hPjwvbGk+PGxpPjxkaXYgY2xhc3M9J0xvZ2luRnJhbWUnPjxkaXYgc3R5bGUgPSdtaW4td2lkdGg6IDEwMHB4Oyc+PC9kaXY+PC9kaXY+PC9saT48L3VsPmRkFSwwNgHSdZ2bG7X5MK3ePxjwI3ZrE7W2esgf8K/1Yqk=";

        private static HttpClient CreateDefaultHttpClient(string? sessionId = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_ADDRESS);
            if (sessionId != null)
            {
                client.DefaultRequestHeaders.Add("Cookie", $"ASP.NET_SessionId={sessionId};");
            }
            return client;
        }

        public static async Task<string?> GenerateNewSessionId()
        {
            var client = CreateDefaultHttpClient();
            HttpResponseMessage response = await client.GetAsync($"/PageDangNhap.aspx");

            string[] cookieHeaderList = new string[2] { "Set-Cookie", "Cookie" };
            foreach (string cookieHeader in cookieHeaderList)
            {
                if (response.Headers.TryGetValues(cookieHeader, out var cookieValue))
                {
                    foreach (var d in cookieValue)
                    {
                        string[] d1 = d.Split(";");
                        foreach (string d2 in d1)
                        {
                            string[] d3 = d2.Split("=");
                            if (d3.Length != 2)
                                continue;
                            if (d3[0] == "ASP.NET_SessionId")
                                return d3[1];
                        }
                    }
                    break;
                }
            }

            return null;
        }

        public static async Task<LoginStatus> Login(string sessionId, string username, string password)
        {
            FormUrlEncodedContent CreateLoginParameters(string username, string password)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { "__VIEWSTATE", __VIEWSTATE },
                    { "__VIEWSTATEGENERATOR", "20CC0D2F" },
                    { "_ctl0:MainContent:DN_txtAcc", username },
                    { "_ctl0:MainContent:DN_txtPass", password },
                    { "_ctl0:MainContent:QLTH_btnLogin", "Đăng+nhập" }
                };
                return new FormUrlEncodedContent(dict);
            }

            var client = CreateDefaultHttpClient(sessionId);
            await client.PostAsync($"/PageDangNhap.aspx", CreateLoginParameters(username, password));
            return await IsLoggedIn(sessionId);
        }

        public static async Task<LoginStatus> IsLoggedIn(string sessionId)
        {
            var client = CreateDefaultHttpClient(sessionId);
            HttpResponseMessage response = await client.GetAsync($"/WebAjax/evLopHP_Load.aspx?E=TTKBLoad&Code=2120");
            if (response.IsSuccessStatusCode)
                return LoginStatus.LoggedIn;
            else return LoginStatus.LoggedOut;

        }

        public static async Task Logout(string sessionId)
        {
            var client = CreateDefaultHttpClient(sessionId);
            await client.GetAsync($"/PageLogout.aspx");
        }

        public static async Task<List<SubjectSchedule>?> GetSubjectScheduleList(string sessionId, int year = 20, int semester = 1)
        {
            if (semester < 1 || semester > 3)
                throw new ArgumentException();

            List<SubjectSchedule>? result = new List<SubjectSchedule>();

            var client = CreateDefaultHttpClient(sessionId);
            HttpResponseMessage response = await client.GetAsync($"/WebAjax/evLopHP_Load.aspx?E=TTKBLoad&Code={year}{(semester <= 2 ? semester : 2)}{(semester == 3 ? 1 : 0)}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("The request has return code {0}.", response.StatusCode));

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

            // TODO: Schedule Study
            var docStudy = document.GetElementById("TTKB_GridInfo");
            var rowListStudy = docStudy.GetElementsByClassName("GridRow").ToList();

            if (rowListStudy != null && rowListStudy.Count > 0)
            {
                foreach (var row in rowListStudy)
                {
                    try
                    {
                        var cellCollection = row.GetElementsByClassName("GridCell").ToList();

                        SubjectSchedule item = new SubjectSchedule
                        {
                            ID = cellCollection[1].TextContent,
                            Name = cellCollection[2].TextContent,
                            Credit = cellCollection[3].TextContent.SafeConvertToFloat(),
                            IsHighQuality = cellCollection[5].IsGridChecked(),
                            Lecturer = cellCollection[6].TextContent,
                            ScheduleStudy = cellCollection[7].TextContent.Split("; ").Select(item1 =>
                            {
                                return new ScheduleItem(
                                    item1.Split(",")[0].StartsWith("Thứ ") ? Convert.ToInt32(item1.Split(",")[0].Remove(0, 4)) : 1,
                                    new Range(
                                        Convert.ToInt32(item1.Split(",")[1].Split("-")[0]),
                                        Convert.ToInt32(item1.Split(",")[1].Split("-")[1])
                                        ),
                                    item1.Split(",")[2]
                                    );
                            }).ToList(),
                            Weeks = cellCollection[8].TextContent.Split(";").Select(item1 =>
                            {
                                return new Range(
                                        Convert.ToInt32(item1.Split("-")[0]),
                                        Convert.ToInt32(item1.Split("-")[1])
                                    );
                            }).ToList(),
                            PointFomula = cellCollection[10].TextContent
                        };

                        result.Add(item);
                    }
                    catch
                    {
                        // TODO: Print error here!
                    }
                }
            }

            // TODO: Schedule Examination
            var docExam = document.GetElementById("TTKB_GridLT");
            var rowListExam = docExam.GetElementsByClassName("'GridRow").ToList();
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

                        item.GroupExam = cellCollection[3].TextContent;
                        item.IsGlobalExam = cellCollection[4].ClassList.Contains("GridCheck");
                        item.DateExamInString = cellCollection[5].TextContent;

                        if (item.DateExamInString == null)
                            continue;

                        DateTime? dateTime = null;
                        string[] splited = item.DateExamInString.Split(new string[] { ", " }, StringSplitOptions.None);
                        string? time = null;
                        for (int i = 0; i < splited.Length; i++)
                        {
                            switch (splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[0])
                            {
                                case "Phòng":
                                    item.RoomExam = splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[1];
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
                            item.DateExamInUnix = (long)dateTime.Value.Subtract(new DateTime(1970, 1, 1)).Add(new TimeSpan(-7, 0, 0)).TotalSeconds;
                        }
                    }
                    catch
                    {
                        // TODO: Print error here!
                    }
                }
            }

            return result;
        }

        public static async Task<List<SubjectFee>?> GetSubjectFeeList(string sessionId, int year = 20, int semester = 1)
        {
            if (semester < 1 || semester > 3)
                throw new ArgumentException();

            List<SubjectFee>? result = new List<SubjectFee>();

            var client = CreateDefaultHttpClient(sessionId);
            HttpResponseMessage response = await client.GetAsync($"/WebAjax/evLopHP_Load.aspx?E=THPhiLoad&Code={year}{(semester <= 2 ? semester : 2)}{(semester == 3 ? 1 : 0)}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(String.Format("The request has return code {0}.", response.StatusCode));

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

            var htmlDocFee = document.GetElementById("THocPhi_GridInfo");
            var rowListFee = htmlDocFee.GetElementsByClassName("GridRow").ToList();

            if (rowListFee != null && rowListFee.Count > 0)
            {
                foreach (var row in rowListFee)
                {
                    try
                    {
                        var cellCollection = row.GetElementsByClassName("GridCell").ToList();

                        SubjectFee item = new SubjectFee
                        {
                            ID = cellCollection[1].TextContent,
                            Name = cellCollection[2].TextContent,
                            Credit = cellCollection[3].TextContent.SafeConvertToFloat(),
                            IsHighQuality = cellCollection[4].ClassList.Contains("GridCheck"),
                            Price = cellCollection[5].TextContent.Replace(",", null).SafeConvertToDouble(),
                            IsDebt = cellCollection[6].ClassList.Contains("GridCheck"),
                            IsReStudy = cellCollection[7].ClassList.Contains("GridCheck"),
                            ConfirmedPaymentAtPhase = cellCollection[8].TextContent
                        };

                        result.Add(item);
                    }
                    catch
                    {
                        // TODO: Print error here!
                    }
                }
            }

            return result;
        }

        public static async Task<AccountInformation?> GetAccountInformation(string sessionId)
        {
            string? GetIDFromTitleBar(string? stringTitleBar)
            {
                try
                {
                    if (stringTitleBar == null)
                    {
                        throw new Exception("stringTitleBar is null here!");
                    }

                    int openCase = stringTitleBar.IndexOf("(");
                    int closeCase = stringTitleBar.IndexOf(")");
                    int stringLength = (stringTitleBar.IndexOf(")")) - (stringTitleBar.IndexOf("(") + 1);
                    return stringTitleBar.Substring(openCase + 1, stringLength);
                }
                catch
                {
                    return null;
                }
            }

            AccountInformation? accInfo = new AccountInformation();

            try
            {
                var client = CreateDefaultHttpClient(sessionId);
                HttpResponseMessage response = await client.GetAsync($"/PageCaNhan.aspx");

                if (!response.IsSuccessStatusCode)
                    throw new Exception(String.Format("The request has return code {0}.", response.StatusCode));

                var config = Configuration.Default;
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

                accInfo.ID = GetIDFromTitleBar(document.GetElementById("Main_lblHoTen").GetTextContent());
                accInfo.Name = document.GetElementById("CN_txtHoTen").GetValue();
                accInfo.DateOfBirth = document.GetElementById("CN_txtNgaySinh").ConvertToDateTime();
                accInfo.DatePlace = document.GetElementById("CN_cboNoiSinh").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.Ethnicity = document.GetElementById("CN_cboDanToc").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.Nationality = document.GetElementById("CN_cboQuocTich").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.Religion = document.GetElementById("CN_cboTonGiao").GetSelectedOptionOnSelectTag().GetTextContent();
                switch (document.GetElementById("CN_txtGioiTinh").GetValue()?.ToLower())
                {
                    case "nam":
                        accInfo.Gender = Gender.Male;
                        break;
                    case "nữ":
                        accInfo.Gender = Gender.Female;
                        break;
                    default:
                        accInfo.Gender = Gender.Unknown;
                        break;
                }

                accInfo.NationalCardID = document.GetElementById("CN_txtSoCMND").GetValue();
                accInfo.NationalCardIssueDate = document.GetElementById("CN_txtNgayCap").ConvertToDateTime();
                accInfo.NationalCardIssuePlace = document.GetElementById("CN_cboNoiCap").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.CitizenCardID = document.GetElementById("CN_txtSoCCCD").GetValue();
                accInfo.CitizenCardIssueDate = document.GetElementById("CN_txtNcCCCD").ConvertToDateTime();

                accInfo.HealthInsuranceID = document.GetElementById("CN_txtSoBHYT").GetValue();
                accInfo.HealthInsuranceExpirationDate = document.GetElementById("CN_txtHanBHYT").ConvertToDateTime();

                accInfo.ClassName = document.GetElementById("CN_txtLop").GetValue();
                accInfo.Specialization = document.GetElementById("MainContent_CN_txtNganh").GetValue();
                accInfo.TrainingProgramPlan = document.GetElementById("MainContent_CN_txtCTDT").GetValue();
                accInfo.TrainingProgramPlan2 = document.GetElementById("MainContent_CN_txtCT2").GetValue();

                accInfo.SchoolEmail = document.GetElementById("CN_txtMail1").GetValue();
                accInfo.PersonalEmail = document.GetElementById("CN_txtMail2").GetValue();
                accInfo.FacebookLink = document.GetElementById("CN_txtFace").GetValue();
                accInfo.PhoneNumber = document.GetElementById("CN_txtPhone").GetValue();

                accInfo.BankInfo =
                    string.Format(
                        "{0} ({1})",
                        document.GetElementById("CN_txtTKNHang").GetValue(),
                        document.GetElementById("CN_txtNgHang").GetValue()
                        );
            }
            catch
            {
                accInfo = null;
            }

            return accInfo;
        }

        public static async Task<AccountTrainingResult?> GetAccountTrainingResult(string sessionId)
        {
            AccountTrainingResult? result = null;

            try
            {
                var client = CreateDefaultHttpClient(sessionId);
                HttpResponseMessage response = await client.GetAsync($"/PageKQRL.aspx");

                if (!response.IsSuccessStatusCode)
                    throw new Exception(String.Format("The request has return code {0}.", response.StatusCode));

                var config = Configuration.Default;
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

                TrainingSummary trainSum = new TrainingSummary();
                var docTrainList = document.GetElementById("KQRLGridTH").GetElementsByClassName("GridRow");
                foreach (var item in docTrainList)
                {
                    var cellList = item.GetElementsByClassName("GridCell");
                    if (cellList[0].GetTextContent().IsNullOrEmpty() ||
                        cellList[cellList.Length - 1].GetTextContent().IsNullOrEmpty() ||
                        cellList[cellList.Length - 2].GetTextContent().IsNullOrEmpty() ||
                        cellList[cellList.Length - 3].GetTextContent().IsNullOrEmpty()
                        ) continue;

                    if (trainSum.SchoolYearStart.IsNullOrEmpty())
                    {
                        trainSum.SchoolYearStart = cellList[0].GetTextContent();
                    }

                    trainSum.SchoolYearCurrent = cellList[0].GetTextContent();
                    trainSum.CreditCollected = Convert.ToDouble(cellList[cellList.Length - 3].GetTextContent());
                    trainSum.AvgTrainingScore4 = Convert.ToDouble(cellList[cellList.Length - 2].GetTextContent());
                    trainSum.AvgSocial = Convert.ToDouble(cellList[cellList.Length - 1].GetTextContent());

                }

                GraduateSummary gradSum = new GraduateSummary();
                var docGrad = document.GetElementById("KQRLdvCc").ConvertToIDocument();
                gradSum.HasSigGDTC = docGrad.GetElementById("KQRL_chkGDTC").IsSelectedInInput();
                gradSum.HasSigGDQP = docGrad.GetElementById("KQRL_chkQP").IsSelectedInInput();
                gradSum.HasSigEnglish = docGrad.GetElementById("KQRL_chkCCNN").IsSelectedInInput();
                gradSum.HasSigIT = docGrad.GetElementById("KQRL_chkCCTH").IsSelectedInInput();
                gradSum.HasQualifiedGraduate = docGrad.GetElementById("KQRL_chkCNTN").IsSelectedInInput();
                gradSum.Info1 = docGrad.GetElementById("KQRL_txtKT").GetTextContent();
                gradSum.Info2 = docGrad.GetElementById("KQRL_txtKL").GetTextContent();
                gradSum.Info3 = docGrad.GetElementById("KQRL_txtInfo").GetTextContent();
                gradSum.ApproveGraduateProcessInfo = docGrad.GetElementById("KQRL_txtCNTN").GetTextContent();

                List<SubjectResult> subSum = new List<SubjectResult>();
                var docSub = document.GetElementById("KQRL_divContent")
                    .ConvertToIDocument()
                    .GetElementById("KQRLGridKQHT")
                    .GetElementsByClassName("GridRow");
                for (int i = docSub.Length - 1; i >= 0; i--)
                {
                    var docCell = docSub[i].GetElementsByClassName("GridCell");

                    var item = new SubjectResult
                    {
                        SchoolYear = docCell[1].GetTextContent(),
                        IsExtendedSemester = docCell[2].ClassList.Contains("GridCheck"),
                        ID = docCell[3].GetTextContent(),
                        Name = docCell[4].GetTextContent(),
                        Credit = docCell[5].GetTextContent().SafeConvertToDouble(),
                        PointFormula = docCell[6].GetTextContent(),
                        PointBT = docCell[7].GetTextContent().SafeConvertToDouble(),
                        PointBV = docCell[8].GetTextContent().SafeConvertToDouble(),
                        PointCC = docCell[9].GetTextContent().SafeConvertToDouble(),
                        PointCK = docCell[10].GetTextContent().SafeConvertToDouble(),
                        PointGK = docCell[11].GetTextContent().SafeConvertToDouble(),
                        PointQT = docCell[12].GetTextContent().SafeConvertToDouble(),
                        PointTH = docCell[13].GetTextContent().SafeConvertToDouble(),
                        PointFinalT10 = docCell[14].GetTextContent().SafeConvertToDouble(),
                        PointFinalT4 = docCell[15].GetTextContent().SafeConvertToDouble(),
                        PointFinalByChar = docCell[16].GetTextContent() ?? "I",
                        IsReStudy = subSum.Any(p =>
                                p.Name.ToLower() == docCell[4].GetTextContent()?.ToLower()
                        )
                    };

                    subSum.Add(item);
                }

                result = new AccountTrainingResult()
                {
                    TrainingSummary = trainSum,
                    GraduateSummary = gradSum,
                    SubjectResultList = subSum
                };
            }
            catch
            {

            }

            return result;
        }
    }
}
