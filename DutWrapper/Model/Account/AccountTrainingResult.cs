using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.Model.Account
{
    public class AccountTrainingResult
    {
        public TrainingSummary TrainingSummary { get; set; } = new TrainingSummary();

        public GraduateSummary GraduateSummary { get; set; } = new GraduateSummary();

        public List<SubjectResult> SubjectResultList { get; set; } = new List<SubjectResult>();
    }

    public class TrainingSummary
    {
        public string? SchoolYearStart { get; set; }

        public string? SchoolYearCurrent { get; set; }

        public double CreditCollected { get; set; } = 0;

        public double AvgTrainingScore4 { get; set; } = 0;

        public double AvgSocial { get; set; } = 0;
    }

    public class GraduateSummary
    {
        public bool HasSigGDTC { get; set; } = false;

        public bool HasSigGDQP { get; set; } = false;

        public bool HasSigEnglish { get; set; } = false;

        public bool HasSigIT { get; set; } = false;

        public bool HasQualifiedGraduate { get; set; } = false;

        public string? Info1 { get; set; } = null;

        public string? Info2 { get; set; } = null;

        public string? Info3 { get; set; } = null;

        public string? ApproveGraduateProcessInfo { get; set; } = null;
    }

    public class SubjectResult
    {
        public string? SchoolYear { get; set; }

        public bool IsExtendedSemester { get; set; } = false;

        public string ID { get; set; }

        public string Name { get; set; }

        public double Credit { get; set; }

        public string? PointFormula { get; set; }

        public double? PointBT { get; set; }

        public double? PointBV { get; set; }

        public double? PointCC { get; set; }

        public double? PointCK { get; set; }

        public double? PointGK { get; set; }

        public double? PointQT { get; set; }

        public double? PointTH { get; set; }

        public double? PointFinalT4 { get; set; }

        public double? PointFinalT10 { get; set; }

        public string PointFinalByChar { get; set; } = "I";

        public bool IsReStudy { get; set; } = false;
    }
}
