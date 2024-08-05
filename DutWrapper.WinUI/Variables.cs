using DutWrapper.WinUI.News;

namespace DutWrapper.WinUI
{
    public static class Variables
    {
        public static class ServerUrl
        {
            public static string DUT_BASEURL = "http://dut.udn.vn";

            public static string DUT_LICHTUANURL = $"{DUT_BASEURL}/Lichtuan";

            public static string DUTSV_BASEURL = "http://sv.dut.udn.vn";

            public static string DUTSV_FETCHNEWSURL(NewsType newsType = null, int page = 1, SearchMethod searchType = null, string searchQuery = null)
            {
                return string.Format(
                    @"{0}/WebAjax/evLopHP_Load.aspx?E={1}&PAGETB={2}&COL={3}&NAME={4}&TAB=1",
                    DUTSV_BASEURL,
                    newsType == null ? NewsType.Global.Value : newsType.Value,
                    page > 0 ? page : 1,
                    searchType == null ? SearchMethod.ByTitle.Value : searchType.Value,
                    searchQuery == null ? "" : searchQuery
                    );
            }

            public static string DUTSV_PAGELOGINURL = $"{DUTSV_BASEURL}/PageDangNhap.aspx";
            public static string DUTSV_PAGELOGOUTURL = $"{DUTSV_BASEURL}/PageLogout.aspx";
            public static string DUTSV_PAGECHECKLOGGEDINURL = $"{DUTSV_BASEURL}/WebAjax/evLopHP_Load.aspx?E=TTKBLoad&Code=2120";

            public static string DUTSV_FETCHSUBJETSCHEDULEURL(Accounts.SchoolYear schoolYear)
            {
                return string.Format(
                    @"{0}/WebAjax/evLopHP_Load.aspx?E=TTKBLoad&Code={1}{2}{3}",
                    DUTSV_BASEURL,
                    schoolYear.Year,
                    schoolYear.Semester > 2 ? 2 : schoolYear.Semester,
                    schoolYear.Semester == 3 ? 1 : 0
                    );
            }

            public static string DUTSV_FETCHSUBJETFEEURL(Accounts.SchoolYear schoolYear)
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
