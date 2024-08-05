using AngleSharp;
using AngleSharp.Dom;
using DutWrapper.CustomHttpClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DutWrapper.Accounts
{
    public static class AccountsInstance
    {
        private static string? GetSessionIdFromCookie(List<Header> headers)
        {
            string[] cookieHeaderList = new string[2] { "Set-Cookie", "Cookie" };
            var cookieList = headers.Where(p => cookieHeaderList.Contains(p.Key)).ToList();

            foreach (Header cookieItem in cookieList)
            {
                List<string> d1 = cookieItem.Value.Split(";").Select(c => c.Trim()).ToList();
                foreach (string d2 in d1)
                {
                    string[] d3 = d2.Split("=");
                    if (d3.Length != 2)
                        continue;
                    if (d3[0] == "ASP.NET_SessionId")
                        return d3[1];
                }
            }

            return null;
        }

        private static Header SessionIdToHeader(string sessionId)
        {
            return new Header("Cookie", string.Format("{0}={1};", "ASP.NET_SessionId", sessionId));
        }

        private static FormUrlEncodedContent CreateLoginFormUrlEncoded(string viewState, string viewStateGenerator, string username, string password)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { "__VIEWSTATE", viewState },
                    { "__VIEWSTATEGENERATOR", viewStateGenerator },
                    { "_ctl0:MainContent:DN_txtAcc", username },
                    { "_ctl0:MainContent:DN_txtPass", password },
                    { "_ctl0:MainContent:QLTH_btnLogin", "Đăng+nhập" }
                };
            return new FormUrlEncodedContent(dict);
        }

        public static async Task<Session> GenerateSessionAsync(List<Header>? headers = null)
        {
            var response = await CustomHttpClientInstance.Get(new Uri("http://sv.dut.udn.vn/PageDangNhap.aspx"), headers);
            response.EnsureNoException();
            var document = await FunctionExtension.AngleSharpHtmlToDocument(response.Content ?? "");
            return new Session(
                sessionId: GetSessionIdFromCookie(response.Headers),
                viewState: document.GetElementById("__VIEWSTATE").GetValue(),
                viewStateGenerator: document.GetElementById("__VIEWSTATEGENERATOR").GetValue()
                );
        }

        public static async Task LoginAsync(Session session, AuthInfo authInfo, List<Header>? headers = null)
        {
            authInfo.EnsureValidAuth();
            session.EnsureValidSession();
            session.EnsureValidViewState();

            var response = await CustomHttpClientInstance.Post(
                new Uri(Variables.ServerUrl.DUTSV_PAGELOGINURL),
                CreateLoginFormUrlEncoded(session.ViewState!, session.ViewStateGenerator!, authInfo.Username!, authInfo.Password!),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureNoException();
        }

        public static async Task LogoutAsync(Session session, List<Header>? headers = null)
        {
            session.EnsureValidSession();

            var response = await CustomHttpClientInstance.Get(
                new Uri(Variables.ServerUrl.DUTSV_PAGELOGOUTURL),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureNoException();
        }

        public static async Task<LoginStatus> IsLoggedInAsync(Session session, List<Header>? headers = null)
        {
            session.EnsureValidSession();

            var response = await CustomHttpClientInstance.Get(
                new Uri(Variables.ServerUrl.DUTSV_PAGECHECKLOGGEDINURL),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureNoException();

            return response.Code / 100 == 2 ? LoginStatus.LoggedIn : LoginStatus.LoggedOut;
        }

        public static async Task<List<SubjectInformation>> FetchSubjectInformationAsync(Session session, SchoolYear schoolYear, List<Header>? headers = null)
        {
            session.EnsureValidSession();
            var response = await CustomHttpClientInstance.Get(
                new Uri(Variables.ServerUrl.DUTSV_FETCHSUBJETSCHEDULEURL(schoolYear)),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureSuccessfulRequest();

            var document = await FunctionExtension.AngleSharpHtmlToDocument(response.Content!);

            try
            {
                List<SubjectInformation> result = new List<SubjectInformation>();

                // Subject study schedule.
                var docStudy = document.GetElementById("TTKB_GridInfo");
                var rowListStudy = docStudy?.GetElementsByClassName("GridRow").ToList();

                if (rowListStudy != null && rowListStudy.Count > 0)
                {
                    foreach (var row in rowListStudy)
                    {
                        try
                        {
                            var cellCollection = row.GetElementsByClassName("GridCell").ToList();

                            SubjectInformation item = new SubjectInformation
                            {
                                ID = cellCollection[1].TextContent,
                                Name = cellCollection[2].TextContent,
                                Credit = cellCollection[3].TextContent.SafeConvertToFloat(),
                                IsHighQuality = cellCollection[5].IsGridChecked(),
                                Lecturer = cellCollection[6].TextContent,
                                ScheduleStudy = new ScheduleStudy(
                                    scheduleList: cellCollection[7].TextContent.Split("; ").Select(item1 =>
                                    {
                                        return new Schedule(
                                            item1.Split(",")[0].StartsWith("Thứ ") ? Convert.ToInt32(item1.Split(",")[0].Remove(0, 4)) : 1,
                                            new Range(
                                                Convert.ToInt32(item1.Split(",")[1].Split("-")[0]),
                                                Convert.ToInt32(item1.Split(",")[1].Split("-")[1])
                                                ),
                                            item1.Split(",")[2]
                                            );
                                    }).ToList(),
                                    weekAffected: cellCollection[8].TextContent.Split(";").Select(item1 =>
                                    {
                                        return new Range(
                                                Convert.ToInt32(item1.Split("-")[0]),
                                                Convert.ToInt32(item1.Split("-")[1])
                                            );
                                    }).ToList()
                                    ),
                                ScheduleExam = new ScheduleExam(),
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
                var rowListExam = docExam?.GetElementsByClassName("'GridRow").ToList();
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

                            ScheduleExam schEx = new ScheduleExam();
                            schEx.GroupExam = cellCollection[3].TextContent;
                            schEx.IsGlobalExam = cellCollection[4].ClassList.Contains("GridCheck");
                            schEx.DateExamInString = cellCollection[5].TextContent;

                            if (schEx.DateExamInString == null)
                                continue;

                            DateTime? dateTime = null;
                            string[] splited = schEx.DateExamInString.Split(new string[] { ", " }, StringSplitOptions.None);
                            string? time = null;
                            for (int i = 0; i < splited.Length; i++)
                            {
                                switch (splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[0])
                                {
                                    case "Phòng":
                                        schEx.RoomExam = splited[i].Split(new string[] { ": " }, StringSplitOptions.None)[1];
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
                                schEx.DateExamInUnix = (long)dateTime.Value.Subtract(new DateTime(1970, 1, 1)).Add(new TimeSpan(-7, 0, 0)).TotalSeconds;
                            }

                            item.ScheduleExam = schEx;
                        }
                        catch
                        {
                            // TODO: Print error here!
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // Exception when parsing subject schedule.
                throw ex;
            }
        }

        public static async Task<List<SubjectFee>> FetchSubjectFeeAsync(Session session, SchoolYear schoolYear, List<Header>? headers = null)
        {
            session.EnsureValidSession();
            var response = await CustomHttpClientInstance.Get(
                new Uri(Variables.ServerUrl.DUTSV_FETCHSUBJETFEEURL(schoolYear)),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureSuccessfulRequest();

            var document = await FunctionExtension.AngleSharpHtmlToDocument(response.Content!);

            try
            {
                List<SubjectFee> result = new List<SubjectFee>();

                var htmlDocFee = document.GetElementById("THocPhi_GridInfo");
                var rowListFee = htmlDocFee?.GetElementsByClassName("GridRow").ToList();

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
                                VerifiedPaymentAt = cellCollection[8].TextContent
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
            catch (Exception ex)
            {
                // Exception when parsing subject fee.
                throw ex;
            }
        }

        public static async Task<StudentInformation> FetchStudentInformationAsync(Session session, List<Header>? headers = null)
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
                    int stringLength = stringTitleBar.IndexOf(")") - (stringTitleBar.IndexOf("(") + 1);
                    return stringTitleBar.Substring(openCase + 1, stringLength);
                }
                catch
                {
                    return null;
                }
            }

            session.EnsureValidSession();
            var response = await CustomHttpClientInstance.Get(
                new Uri(Variables.ServerUrl.DUTSV_ACCOUNTINFOURL),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureSuccessfulRequest();

            var document = await FunctionExtension.AngleSharpHtmlToDocument(response.Content!);

            try
            {
                StudentInformation accInfo = new StudentInformation();
                accInfo.StudentID = GetIDFromTitleBar(document.GetElementById("Main_lblHoTen").GetTextContent());
                accInfo.Name = document.GetElementById("CN_txtHoTen").GetValue();
                accInfo.DateOfBirth = document.GetElementById("CN_txtNgaySinh").ConvertToDateTime();
                accInfo.BirthPlace = document.GetElementById("CN_cboNoiSinh").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.Ethnicity = document.GetElementById("CN_cboDanToc").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.Nationality = document.GetElementById("CN_cboQuocTich").GetSelectedOptionOnSelectTag().GetTextContent();
                accInfo.Religion = document.GetElementById("CN_cboTonGiao").GetSelectedOptionOnSelectTag().GetTextContent();
                switch (document.GetElementById("CN_txtGioiTinh").GetValue()?.ToLower())
                {
                    case "nam":
                        accInfo.Gender = LecturerGender.Male;
                        break;
                    case "nữ":
                        accInfo.Gender = LecturerGender.Female;
                        break;
                    default:
                        accInfo.Gender = LecturerGender.Unknown;
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

                accInfo.BankID = document.GetElementById("CN_txtTKNHang").GetValue();
                accInfo.BankName = document.GetElementById("CN_txtNgHang").GetValue();

                return accInfo;
            }
            catch (Exception ex)
            {
                // Exception when parsing account information.
                throw ex;
            }
        }

        public static async Task<TrainingResult> FetchTrainingResultAsync(Session session, List<Header>? headers = null)
        {
            session.EnsureValidSession();
            var response = await CustomHttpClientInstance.Get(
                new Uri(Variables.ServerUrl.DUTSV_ACCOUNTTRAININGRESULTURL),
                (headers ?? new List<Header>()).Append(SessionIdToHeader(session.SessionId!)).ToList()
                );
            response.EnsureSuccessfulRequest();

            var document = await FunctionExtension.AngleSharpHtmlToDocument(response.Content!);

            try
            {
                // Parsing to Training summary
                TrainingSummary trainSum = new TrainingSummary();
                var docTrainList = document.GetElementById("KQRLGridTH")?.GetElementsByClassName("GridRow");
                if (docTrainList != null)
                {
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
                }

                // Parsing to graduate summary
                GraduateSummary gradSum = new GraduateSummary();
                var docGrad = document.GetElementById("KQRLdvCc").ConvertToIDocument();
                if (docGrad != null)
                {
                    gradSum.HasSigPhysicalEducation = docGrad.GetElementById("KQRL_chkGDTC").IsSelectedInInput();
                    gradSum.HasSigNationalDefenseEducation = docGrad.GetElementById("KQRL_chkQP").IsSelectedInInput();
                    gradSum.HasSigEnglish = docGrad.GetElementById("KQRL_chkCCNN").IsSelectedInInput();
                    gradSum.HasSigIT = docGrad.GetElementById("KQRL_chkCCTH").IsSelectedInInput();
                    gradSum.HasQualifiedGraduate = docGrad.GetElementById("KQRL_chkCNTN").IsSelectedInInput();
                    gradSum.RewardsInfo = docGrad.GetElementById("KQRL_txtKT").GetTextContent();
                    gradSum.Discipline = docGrad.GetElementById("KQRL_txtKL").GetTextContent();
                    gradSum.EligibleGraduationThesisStatus = docGrad.GetElementById("KQRL_txtInfo").GetTextContent();
                    gradSum.EligibleGraduationStatus = docGrad.GetElementById("KQRL_txtCNTN").GetTextContent();
                }

                // Parsing to subject result
                List<SubjectResult> subSum = new List<SubjectResult>();
                var docSub = document.GetElementById("KQRL_divContent")
                    ?.ConvertToIDocument()
                    ?.GetElementById("KQRLGridKQHT")
                    ?.GetElementsByClassName("GridRow");
                if (docSub != null)
                {
                    for (int i = docSub.Length - 1; i >= 0; i--)
                    {
                        var docCell = docSub[i].GetElementsByClassName("GridCell");

                        var item = new SubjectResult
                        {
                            Index = docCell[5].GetTextContent().SafeConvertToInt(),
                            SchoolYear = docCell[1].GetTextContent(),
                            IsExtendedSemester = docCell[2].ClassList.Contains("GridCheck"),
                            ID = docCell[3].GetTextContent() ?? "",
                            Name = docCell[4].GetTextContent() ?? "",
                            Credit = docCell[5].GetTextContent().SafeConvertToDouble(),
                            PointFormula = docCell[6].GetTextContent(),
                            PointBT = docCell[7].GetTextContent().SafeConvertToDouble(),
                            PointBV = docCell[8].GetTextContent().SafeConvertToDouble(),
                            PointCC = docCell[9].GetTextContent().SafeConvertToDouble(),
                            PointCK = docCell[10].GetTextContent().SafeConvertToDouble(),
                            PointGK = docCell[11].GetTextContent().SafeConvertToDouble(),
                            PointQT = docCell[12].GetTextContent().SafeConvertToDouble(),
                            PointTH = docCell[13].GetTextContent().SafeConvertToDouble(),
                            PointTT = docCell[14].GetTextContent().SafeConvertToDouble(),
                            PointFinalT10 = docCell[15].GetTextContent().SafeConvertToDouble(),
                            PointFinalT4 = docCell[16].GetTextContent().SafeConvertToDouble(),
                            PointFinalByChar = docCell[17].GetTextContent() ?? "I",
                            IsReStudy = subSum.Any(p =>
                                    p.Name.ToLower().Contains(docCell[4].GetTextContent()?.ToLower() ?? "???")
                            )
                        };

                        subSum.Add(item);
                    }
                }

                // Return 3 result above
                return new TrainingResult()
                {
                    TrainingSummary = trainSum,
                    GraduateSummary = gradSum,
                    SubjectResultList = subSum
                };
            }
            catch (Exception ex)
            {
                // Exception when parsing account training result.
                throw ex;
            }
        }
    }
}
