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
        private static string __VIEWSTATE = "/wEPDwUKMTY2NjQ1OTEyNA8WAh4TVmFsaWRhdGVSZXF1ZXN0TW9kZQIBFgJmD2QWBAIDDxYCHglpbm5lcmh0bWwFsy48dWwgaWQ9J21lbnUnIHN0eWxlPSd3aWR0aDogMTI4MHB4OyBtYXJnaW46IDAgYXV0bzsgJz48bGk+PGEgSUQ9ICdsUGFIT01FJyBzdHlsZSA9J3dpZHRoOjY1cHgnIGhyZWY9J0RlZmF1bHQuYXNweCc+VHJhbmcgY2jhu6c8L2E+PGxpPjxhIElEPSAnbFBhQ1REVCcgc3R5bGUgPSd3aWR0aDo4NXB4JyBocmVmPScnPkNoxrDGoW5nIHRyw6xuaDwvYT48dWwgY2xhc3M9J3N1Ym1lbnUnPjxsaT48YSBJRCA9J2xDb0NURFRDMicgICBzdHlsZSA9J3dpZHRoOjE0MHB4JyBocmVmPSdHX0xpc3RDVERULmFzcHgnPkNoxrDGoW5nIHRyw6xuaCDEkcOgbyB04bqhbzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NURFRDMScgICBzdHlsZSA9J3dpZHRoOjE0MHB4JyBocmVmPSdHX0xpc3RIb2NQaGFuLmFzcHgnPkjhu41jIHBo4bqnbjwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NURFRDMycgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdHX0xpc3RDVERUQW5oLmFzcHgnPlByb2dyYW08L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIElEPSAnbFBhS0hEVCcgc3R5bGUgPSd3aWR0aDo2MHB4JyBocmVmPScnPkvhur8gaG/huqFjaDwvYT48dWwgY2xhc3M9J3N1Ym1lbnUnPjxsaT48YSBJRCA9J2xDb0tIRFRDMScgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdodHRwOi8vZHV0LnVkbi52bi9UcmFuZ0Rhb3Rhby9HaW9pdGhpZXUvaWQvNzM5NSc+S+G6vyBob+G6oWNoIMSRw6BvIHThuqFvIG7Eg20gaOG7jWM8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzInICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0naHR0cDovL2RrNC5kdXQudWRuLnZuJz7EkMSDbmcga8O9IGjhu41jPC9hPjwvbGk+PGxpPjxhIElEID0nbENvS0hEVEMzJyAgIHN0eWxlID0nd2lkdGg6MjAwcHgnIGhyZWY9J2h0dHA6Ly9kazQuZHV0LnVkbi52bi9HX0xvcEhvY1BoYW4uYXNweCc+TOG7m3AgaOG7jWMgcGjhuqduIC0gxJFhbmcgxJHEg25nIGvDvTwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0tIRFRDNCcgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdHX0xvcEhvY1BoYW4uYXNweCc+TOG7m3AgaOG7jWMgcGjhuqduIC0gY2jDrW5oIHRo4bupYzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0tIRFRDNScgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdodHRwOi8vZGs0LmR1dC51ZG4udm4vR19ES3lOaHVDYXUuYXNweCc+S2jhuqNvIHPDoXQgbmh1IGPhuqd1IG3hu58gdGjDqm0gbOG7m3A8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzYnICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0naHR0cDovL2NiLmR1dC51ZG4udm4vUGFnZUxpY2hUaGlLSC5hc3B4Jz5UaGkgY3Xhu5FpIGvhu7MgbOG7m3AgaOG7jWMgcGjhuqduPC9hPjwvbGk+PGxpPjxhIElEID0nbENvS0hEVEM3JyAgIHN0eWxlID0nd2lkdGg6MjAwcHgnIGhyZWY9J0dfREtUaGlOTi5hc3B4Jz5UaGkgVGnhur9uZyBBbmggxJHhu4tuaCBr4buzLCDEkeG6p3UgcmE8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzgnICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0nR19MaXN0TGljaFNILmFzcHgnPlNpbmggaG/huqF0IGzhu5twIMSR4buLbmgga+G7szwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0tIRFRDOScgICBzdHlsZSA9J3dpZHRoOjIwMHB4JyBocmVmPSdodHRwOi8vZmIuZHV0LnVkbi52bic+S2jhuqNvIHPDoXQgw70ga2nhur9uIHNpbmggdmnDqm48L2E+PC9saT48bGk+PGEgSUQgPSdsQ29LSERUQzknICAgc3R5bGUgPSd3aWR0aDoyMDBweCcgaHJlZj0nR19ES1BWQ0QuYXNweCc+SG/huqF0IMSR4buZbmcgcGjhu6VjIHbhu6UgY+G7mW5nIMSR4buTbmc8L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIElEPSAnbFBhVFJBQ1VVJyBzdHlsZSA9J3dpZHRoOjcwcHgnIGhyZWY9Jyc+RGFuaCBzw6FjaDwvYT48dWwgY2xhc3M9J3N1Ym1lbnUnPjxsaT48YSBJRCA9J2xDb1RSQUNVVTAxJyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdE5ndW5nSG9jLmFzcHgnPlNpbmggdmnDqm4gbmfhu6tuZyBo4buNYzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTAzJyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdExvcC5hc3B4Jz5TaW5oIHZpw6puIMSRYW5nIGjhu41jIC0gbOG7m3A8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUwNCcgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RDQ0NOVFQuYXNweCc+U2luaCB2acOqbiBjw7MgY2jhu6luZyBjaOG7iSBDTlRUPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMDUnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0Q0NOTi5hc3B4Jz5TaW5oIHZpw6puIGPDsyBjaOG7qW5nIGNo4buJIG5nb+G6oWkgbmfhu688L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUwNicgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdodHRwOi8vZGFvdGFvLmR1dC51ZG4udm4vU1YvR19LUXVhQW5oVmFuLmFzcHgnPlNpbmggdmnDqm4gdGhpIFRp4bq/bmcgQW5oIMSR4buLbmgga+G7szwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTA3JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdERvQW5UTi5hc3B4Jz5TaW5oIHZpw6puIGzDoG0gxJDhu5Mgw6FuIHThu5F0IG5naGnhu4dwPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMDgnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0SG9hbkhvY1BoaS5hc3B4Jz5TaW5oIHZpw6puIMSRxrDhu6NjIGhvw6NuIMSRw7NuZyBo4buNYyBwaMOtPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMTYnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0SG9hbl9UaGlCUy5hc3B4Jz5TaW5oIHZpw6puIMSRxrDhu6NjIGhvw6NuIHRoaSwgdGhpIGLhu5Ugc3VuZzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTA5JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdEhvY0xhaS5hc3B4Jz5TaW5oIHZpw6puIGThu7EgdHV54buDbiB2w6BvIGjhu41jIGzhuqFpPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMTAnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0S3lMdWF0LmFzcHgnPlNpbmggdmnDqm4gYuG7iyBr4bu3IGx14bqtdDwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTExJyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdEJpSHV5SFAuYXNweCc+U2luaCB2acOqbiBi4buLIGjhu6d5IGjhu41jIHBo4bqnbjwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTEyJyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdExvY2tXZWIuYXNweCc+U2luaCB2acOqbiBi4buLIGtow7NhIHdlYnNpdGU8L2E+PC9saT48bGk+PGEgSUQgPSdsQ29UUkFDVVUxMycgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RMb2NrV2ViVGFtLmFzcHgnPlNpbmggdmnDqm4gYuG7iyB04bqhbSBraMOzYSB3ZWJzaXRlPC9hPjwvbGk+PGxpPjxhIElEID0nbENvVFJBQ1VVMTQnICAgc3R5bGUgPSd3aWR0aDoyNDBweCcgaHJlZj0nR19MaXN0SGFuQ2hlVEMuYXNweCc+U2luaCB2acOqbiBi4buLIGjhuqFuIGNo4bq/IHTDrW4gY2jhu4kgxJHEg25nIGvDvTwvYT48L2xpPjxsaT48YSBJRCA9J2xDb1RSQUNVVTE1JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfTGlzdENhbmhCYW9LUUhULmFzcHgnPlNpbmggdmnDqm4gYuG7iyBj4bqjbmggYsOhbyBr4bq/dCBxdeG6oyBo4buNYyB04bqtcDwvYT48L2xpPjwvdWw+PC9saT48bGk+PGEgSUQ9ICdsUGFDVVVTVicgc3R5bGUgPSd3aWR0aDo4OHB4JyBocmVmPScnPkPhu7F1IHNpbmggdmnDqm48L2E+PHVsIGNsYXNzPSdzdWJtZW51Jz48bGk+PGEgSUQgPSdsQ29DVVVTVjEnICAgc3R5bGUgPSd3aWR0aDoxMTBweCcgaHJlZj0nR19MaXN0VE5naGllcC5hc3B4Jz7EkMOjIHThu5F0IG5naGnhu4dwPC9hPjwvbGk+PGxpPjxhIElEID0nbENvQ1VVU1YyJyAgIHN0eWxlID0nd2lkdGg6MTEwcHgnIGhyZWY9J0dfTGlzdEtob25nVE4uYXNweCc+S2jDtG5nIHThu5F0IG5naGnhu4dwPC9hPjwvbGk+PC91bD48L2xpPjxsaT48YSBJRD0gJ2xQYUNTVkMnIHN0eWxlID0nd2lkdGg6MTQ1cHgnIGhyZWY9Jyc+UGjDsm5nIGjhu41jICYgSOG7hyB0aOG7kW5nPC9hPjx1bCBjbGFzcz0nc3VibWVudSc+PGxpPjxhIElEID0nbENvQ1NWQzAxJyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J2h0dHA6Ly9jYi5kdXQudWRuLnZuL1BhZ2VDTlBob25nSG9jLmFzcHgnPlTDrG5oIGjDrG5oIHPhu60gZOG7pW5nIHBow7JuZyBo4buNYzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0NTVkMwMicgICBzdHlsZSA9J3dpZHRoOjI0MHB4JyBocmVmPSdHX0xpc3RUaEJpSG9uZy5hc3B4Jz5UaOG7kW5nIGvDqiBiw6FvIHRoaeG6v3QgYuG7iyBwaMOybmcgaOG7jWMgaOG7j25nPC9hPjwvbGk+PGxpPjxhIElEID0nbENvQ1NWQzA5JyAgIHN0eWxlID0nd2lkdGg6MjQwcHgnIGhyZWY9J0dfU3lzU3RhdHVzLmFzcHgnPlRy4bqhbmcgdGjDoWkgaOG7hyB0aOG7kW5nIHRow7RuZyB0aW4gc2luaCB2acOqbjwvYT48L2xpPjwvdWw+PC9saT48bGk+PGEgSUQ9ICdsUGFMSUVOS0VUJyBzdHlsZSA9J3dpZHRoOjUwcHgnIGhyZWY9Jyc+TGnDqm4ga+G6v3Q8L2E+PHVsIGNsYXNzPSdzdWJtZW51Jz48bGk+PGEgSUQgPSdsQ29MSUVOS0VUMScgICBzdHlsZSA9J3dpZHRoOjcwcHgnIGhyZWY9J2h0dHA6Ly9saWIuZHV0LnVkbi52bic+VGjGsCB2aeG7h248L2E+PC9saT48bGk+PGEgSUQgPSdsQ29MSUVOS0VUMicgICBzdHlsZSA9J3dpZHRoOjcwcHgnIGhyZWY9J2h0dHA6Ly9sbXMxLmR1dC51ZG4udm4nPkRVVC1MTVM8L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIElEPSAnbFBhSEVMUCcgc3R5bGUgPSd3aWR0aDo0NXB4JyBocmVmPScnPkjhu5cgdHLhu6M8L2E+PHVsIGNsYXNzPSdzdWJtZW51Jz48bGk+PGEgSUQgPSdsQ29IRUxQMScgICBzdHlsZSA9J3dpZHRoOjIxMHB4JyBocmVmPSdodHRwOi8vZnIuZHV0LnVkbi52bic+Q+G7lW5nIGjhu5cgdHLhu6MgdGjDtG5nIHRpbiB0cuG7sWMgdHV54bq/bjwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0hFTFAyJyAgIHN0eWxlID0nd2lkdGg6MjEwcHgnIGhyZWY9J2h0dHBzOi8vMWRydi5tcy91L3MhQXR3S2xEWjZWcWJ0cUVvRU9lNEROeHY1LWVRND9lVWJ4Sm5xJz5IxrDhu5tuZyBk4bqrbiDEkMSDbmcga8O9IGjhu41jPC9hPjwvbGk+PGxpPjxhIElEID0nbENvSEVMUDMnICAgc3R5bGUgPSd3aWR0aDoyMTBweCcgaHJlZj0naHR0cHM6Ly8xZHJ2Lm1zL3UvcyFBdHdLbERaNlZxYnRxRW9FT2U0RE54djUtZVE0P2VVYnhKbnEnPkjGsOG7m25nIGThuqtuIFPhu60gZOG7pW5nIEVtYWlsIERVVDwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0hFTFA3JyAgIHN0eWxlID0nd2lkdGg6MjEwcHgnIGhyZWY9J2h0dHBzOi8vMWRydi5tcy91L3MhQXR3S2xEWjZWcWJ0bzEwYmhIYzBLN3NleU5Hcj9lYUNUYjh4Jz5WxINuIGLhuqNuIFF1eSDEkeG7i25oIGPhu6dhIFRyxrDhu51uZzwvYT48L2xpPjxsaT48YSBJRCA9J2xDb0hFTFA4JyAgIHN0eWxlID0nd2lkdGg6MjEwcHgnIGhyZWY9J2h0dHBzOi8vZG9jcy5nb29nbGUuY29tL2RvY3VtZW50L2QvMVhFaC1jbGhhNnlueUdyaDVNQWpIZU4wWDIwRDVJWHp5L2VkaXQ/dXNwc2hhcmluZyZvdWlkMTA3MTI5OTI2NDYxOTQxNzgwOTY1JnJ0cG9mdHJ1ZSZzZHRydWUnPkJp4buDdSBt4bqrdSB0aMaw4budbmcgZMO5bmc8L2E+PC9saT48L3VsPjwvbGk+PGxpPjxhIGlkID0nbGlua0RhbmdOaGFwJyBocmVmPSdQYWdlRGFuZ05oYXAuYXNweCcgc3R5bGUgPSd3aWR0aDo4MHB4Oyc+IMSQxINuZyBuaOG6rXAgPC9hPjwvbGk+PGxpPjxkaXYgY2xhc3M9J0xvZ2luRnJhbWUnPjxkaXYgc3R5bGUgPSdtaW4td2lkdGg6IDEwMHB4Oyc+PC9kaXY+PC9kaXY+PC9saT48L3VsPmQCBQ9kFgICAQ9kFgICBw8PFgIeB1Zpc2libGVnZGRkMXi9DoRkvEyw403QVMxAOYudFq4nbc/0X/cYpCA5OUE=";

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
