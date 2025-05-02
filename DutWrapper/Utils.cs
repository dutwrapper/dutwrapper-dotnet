using AngleSharp.Dom;
using DutWrapper.CustomHttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static class Utils
    {
        public static async Task<DutSchoolYear> GetCurrentSchoolYear()
        {
            var response = await CustomHttpClientInstance.Get(new Uri(Variables.ServerUrl.DUT_LICHTUANURL));
            response.EnsureSuccessfulRequest();

            var document = await WebParsingUtils.AngleSharpHtmlToDocument(response.Content!);

            // Area for fetch school year
            var i1 = document.GetElementById("dnn_ctr442_View_cboNamhoc")?.GetSelectedOptionOnSelectTag();
            if (i1 == null)
            {
                // TODO: Throw here!
                throw new Exception();
            }
            // TODO: Need double-check school year here!
            var yearValue = i1.GetValue() ?? "";

            // Area for fetch current week
            var weekList = document.GetElementById("dnn_ctr442_View_cboTuan")?.GetOptionListOnSelectTag();
            var firstWeekString = weekList.Where(p => p.GetTextContent()?.ToLower().Contains("tuần thứ 1:") ?? false).FirstOrDefault();
            if (firstWeekString == null)
            {
                // TODO: Throw here!
                throw new Exception();
            }

            MatchCollection mc = Regex.Matches(
                firstWeekString.GetTextContent(),
                @"Tuần thứ (\d{1,2}): (\d{1,2}\/\d{1,2}\/\d{4})",
                RegexOptions.Multiline
                );
            if (mc.Count < 1)
            {
                // TODO: Throw here!
                throw new Exception();
            }

            // Get DateTime from first week (week 1)
            if (mc[0].Groups.Count != 3)
            {
                // TODO: Throw here!
                throw new Exception();
            }
            DateTimeOffset firstWeekDt = new DateTimeOffset(
                dateTime: new DateTime(
                    Convert.ToInt32(mc[0].Groups[2].Value.Split("/")[2]),
                    Convert.ToInt32(mc[0].Groups[2].Value.Split("/")[1]),
                    Convert.ToInt32(mc[0].Groups[2].Value.Split("/")[0]),
                    0, 0, 0,
                    DateTimeKind.Utc
                    )).AddHours(-7);

            // Get duration (useful for calculate current week).
            long durationCurrentSchoolYear = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - firstWeekDt.ToUnixTimeMilliseconds();
            int currentWeek = Convert.ToInt32(durationCurrentSchoolYear / (1000 * 60 * 60 * 24 * 7) + 1);

            return new DutSchoolYear(
                week: currentWeek,
                schoolYear: yearValue.SafeConvertToInt(),
                dayOfWeek: (int)DateTime.UtcNow.AddHours(7).DayOfWeek + 1,
                firstDateOfSchoolYear: firstWeekDt.UtcDateTime
                );
        }

        public static class Connections
        {
            public static bool IsNetworkAvailable()
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }

            public static bool IsConnectedToInternet()
            {
                if (!IsNetworkAvailable())
                {
                    return false;
                }

                try
                {
                    using Ping ping = new Ping();
                    PingReply reply = ping.Send("example.com", 3000);
                    return reply.Status == IPStatus.Success;
                }
                catch
                {
                    return false;
                }
            }

            public static async Task<bool> IsWebsiteOnline()
            {
                var handler = new HttpClientHandler
                {
                    AllowAutoRedirect = false // Block redirects
                };

                using HttpClient client = new HttpClient(handler)
                {
                    Timeout = TimeSpan.FromSeconds(15)
                };

                try
                {
                    var response = await client.SendAsync(
                        new HttpRequestMessage(HttpMethod.Get, Variables.ServerUrl.DUTSV_BASEURL),
                        HttpCompletionOption.ResponseHeadersRead
                    );

                    int statusCode = (int)response.StatusCode;
                    return statusCode >= 200 && statusCode < 300;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
