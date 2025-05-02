using DutWrapper.Accounts;
using DutWrapper.News;

namespace DutWrapper
{
    public static class Variables
    {
        public static class ServerUrl
        {
            public static string DUT_BASEURL = "http://dut.udn.vn";

            public static string DUT_LICHTUANURL = $"{DUT_BASEURL}/Lichtuan";

            public static string DUTSV_BASEURL = "http://sv.dut.udn.vn";

            public static string DUTSV_FETCHNEWSURL(
                NewsParameters.NewsType newsType = NewsParameters.NewsType.Global,
                int page = 1,
                NewsParameters.SearchMethod searchType = NewsParameters.SearchMethod.ByTitle,
                string? searchQuery = null
                )
            {
                // CTRTBSV, CTRTBGV
                return string.Format(
                    @"{0}/WebAjax/evLopHP_Load.aspx?E={1}&PAGETB={2}&COL={3}&NAME={4}&TAB={5}",
                    DUTSV_BASEURL,
                    newsType == NewsParameters.NewsType.Subject ? "CTRTBGV" : "CTRTBSV",
                    page > 0 ? page : 1,
                    searchType == NewsParameters.SearchMethod.ByTitle ? "TieuDe" : "NoiDung",
                    searchQuery == null ? "" : searchQuery,
                    newsType.ToValue()
                    );
            }

            public static string DUTSV_PAGELOGINURL = $"{DUTSV_BASEURL}/PageDangNhap.aspx";
            public static string DUTSV_PAGELOGOUTURL = $"{DUTSV_BASEURL}/PageLogout.aspx";
            public static string DUTSV_PAGECHECKLOGGEDINURL = $"{DUTSV_BASEURL}/WebAjax/evLopHP_Load.aspx?E=TTKBLoad&Code=2120";

            public static string DUTSV_FETCHSUBJETSCHEDULEURL(SchoolYear schoolYear)
            {
                return string.Format(
                    @"{0}/WebAjax/evLopHP_Load.aspx?E=TTKBLoad&Code={1}{2}{3}",
                    DUTSV_BASEURL,
                    schoolYear.Year,
                    schoolYear.Semester > 2 ? 2 : schoolYear.Semester,
                    schoolYear.Semester == 3 ? 1 : 0
                    );
            }

            public static string DUTSV_FETCHSUBJETFEEURL(SchoolYear schoolYear)
            {
                return string.Format(
                    @"{0}/WebAjax/evLopHP_Load.aspx?E=THPhiLoad&Code={1}{2}{3}",
                    DUTSV_BASEURL,
                    schoolYear.Year,
                    schoolYear.Semester > 2 ? 2 : schoolYear.Semester,
                    schoolYear.Semester == 3 ? 1 : 0
                    );
            }

            public static string DUTSV_ACCOUNTINFOURL = $"{DUTSV_BASEURL}/PageCaNhan.aspx";
            public static string DUTSV_ACCOUNTTRAININGRESULTURL = $"{DUTSV_BASEURL}/PageKQRL.aspx";
        }
    }
}
