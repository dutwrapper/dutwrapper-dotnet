using AngleSharp;
using DutWrapper.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DutWrapper
{
    public static partial class Utils
    {
        public static async Task<DutSchoolYearItem> GetCurrentSchoolWeek()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://dut.udn.vn")
            };
            HttpResponseMessage response = await client.GetAsync("/Lichtuan");

            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(response.Content.ReadAsStringAsync().Result));

            var i1 = document.GetElementById("dnn_ctr442_View_cboNamhoc").GetSelectedOptionOnSelectTag();
            if (i1 == null)
            {
                // TODO: Throw here!
                throw new Exception();
            }

            var year = i1.GetTextContent();
            var yearValue = i1.GetValue();

            var i2 = document.GetElementById("dnn_ctr442_View_cboTuan").GetSelectedOptionOnSelectTag();
            if (i2 == null)
            {
                // TODO: Throw here!
                throw new Exception();
            }
            MatchCollection mc = Regex.Matches(
                i2.GetTextContent(),
                "Tuần thứ (\\d{1,2}): (\\d{1,2}\\/\\d{1,2}\\/\\d{4})",
                RegexOptions.Multiline
                );
            if (mc.Count < 1)
            {
                // TODO: Throw here!
                throw new Exception();
            }
            var week = mc[0].Groups[1].Value;

            return new DutSchoolYearItem(
                week.SafeConvertToInt(),
                year ?? "",
                yearValue.SafeConvertToInt()
                );
        }
    }
}
