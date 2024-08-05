using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.WinUI.Accounts
{
    public class TrainingResult
    {
        [JsonPropertyName("training_summary")]
        public TrainingSummary TrainingSummary { get; set; } = new TrainingSummary();

        [JsonPropertyName("graduate_status")]
        public GraduateSummary GraduateSummary { get; set; } = new GraduateSummary();

        [JsonPropertyName("subject_result")]
        public List<SubjectResult> SubjectResultList { get; set; } = new List<SubjectResult>();
    }

    public class TrainingSummary
    {
        [JsonPropertyName("school_year_start")]
        public string? SchoolYearStart { get; set; }

        [JsonPropertyName("school_year_current")]
        public string? SchoolYearCurrent { get; set; }

        [JsonPropertyName("credit_collected")]
        public double CreditCollected { get; set; } = 0;

        [JsonPropertyName("avg_train_score_4")]
        public double AvgTrainingScore4 { get; set; } = 0;

        [JsonPropertyName("avg_social")]
        public double AvgSocial { get; set; } = 0;
    }

    public class GraduateSummary
    {
        [JsonPropertyName("has_sig_physical_education")]
        public bool HasSigPhysicalEducation { get; set; } = false;

        [JsonPropertyName("has_sig_national_defense_education")]
        public bool HasSigNationalDefenseEducation { get; set; } = false;

        [JsonPropertyName("has_sig_english")]
        public bool HasSigEnglish { get; set; } = false;

        [JsonPropertyName("has_sig_it")]
        public bool HasSigIT { get; set; } = false;

        [JsonPropertyName("has_qualified_graduate")]
        public bool HasQualifiedGraduate { get; set; } = false;

        [JsonPropertyName("rewards_info")]
        public string? RewardsInfo { get; set; } = null;

        [JsonPropertyName("discipline_info")]
        public string? Discipline { get; set; } = null;

        [JsonPropertyName("eligible_graduation_thesis_status")]
        public string? EligibleGraduationThesisStatus { get; set; } = null;

        [JsonPropertyName("eligible_graduation_status")]
        public string? EligibleGraduationStatus { get; set; } = null;
    }

    public class SubjectResult
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("school_year")]
        public string? SchoolYear { get; set; }

        [JsonPropertyName("is_extended_summer")]
        public bool IsExtendedSemester { get; set; } = false;

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("credit")]
        public double Credit { get; set; }

        [JsonPropertyName("point_formula")]
        public string? PointFormula { get; set; }

        [JsonPropertyName("point_bt")]
        public double? PointBT { get; set; }

        [JsonPropertyName("point_bv")]
        public double? PointBV { get; set; }

        [JsonPropertyName("point_cc")]
        public double? PointCC { get; set; }

        [JsonPropertyName("point_ck")]
        public double? PointCK { get; set; }

        [JsonPropertyName("point_gk")]
        public double? PointGK { get; set; }

        [JsonPropertyName("point_qt")]
        public double? PointQT { get; set; }

        [JsonPropertyName("point_th")]
        public double? PointTH { get; set; }

        [JsonPropertyName("point_tt")]
        public double? PointTT { get; set; }

        [JsonPropertyName("result_t4")]
        public double? PointFinalT4 { get; set; }

        [JsonPropertyName("result_t10")]
        public double? PointFinalT10 { get; set; }

        [JsonPropertyName("result_by_char")]
        public string PointFinalByChar { get; set; } = "I";

        [JsonPropertyName("is_restudy")]
        public bool IsReStudy { get; set; } = false;
    }
}
