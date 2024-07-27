using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public static partial class Account
    {
        public class StudentInformation
        {
            /// <summary>
            /// Student name
            /// </summary>
            [JsonPropertyName("name")]
            public string? Name { get; set; } = null;

            /// <summary>
            /// Student account ID
            /// </summary>
            [JsonPropertyName("student_id")]
            public string? StudentID { get; set; } = null;

            /// <summary>
            /// Student date of birth
            /// </summary>
            [JsonPropertyName("date_of_birth")]
            public DateTime DateOfBirth { get; set; }

            /// <summary>
            /// Student birth place
            /// </summary>
            [JsonPropertyName("birth_pace")]
            public string? BirthPlace { get; set; }

            /// <summary>
            /// Student gender
            /// </summary>
            [JsonPropertyName("gender")]
            public LecturerGender Gender { get; set; } = LecturerGender.Unknown;

            /// <summary>
            /// Student ethnicity
            /// </summary>
            [JsonPropertyName("ethnicity")]
            public string? Ethnicity { get; set; } = null;

            /// <summary>
            /// Student nationality
            /// </summary>
            [JsonPropertyName("nationality")]
            public string? Nationality { get; set; } = null;

            /// <summary>
            /// Student religion
            /// </summary>
            [JsonPropertyName("religion")]
            public string? Religion { get; set; } = null;

            /// <summary>
            /// Student national card ID
            /// </summary>
            [JsonPropertyName("national_id_card")]
            public string? NationalCardID { get; set; } = null;

            /// <summary>
            /// Student national card issue date
            /// </summary>
            [JsonPropertyName("national_id_card_issue_date")]
            public DateTime NationalCardIssueDate { get; set; }

            /// <summary>
            /// Student national card issue place
            /// </summary>
            [JsonPropertyName("national_id_card_issue_place")]
            public string? NationalCardIssuePlace { get; set; } = null;

            /// <summary>
            /// Student citizen card ID (replace for national card)
            /// </summary>
            [JsonPropertyName("citizen_id_card")]
            public string? CitizenCardID { get; set; } = null;

            /// <summary>
            /// Student citizen card issue date.
            /// </summary>
            [JsonPropertyName("citizen_id_card_issue_date")]
            public DateTime CitizenCardIssueDate { get; set; }

            /// <summary>
            /// Student bank ID
            /// </summary>
            [JsonPropertyName("account_bank_id")]
            public string? BankID { get; set; } = null;

            /// <summary>
            /// Student bank name
            /// </summary>
            [JsonPropertyName("account_bank_name")]
            public string? BankName { get; set; } = null;

            /// <summary>
            /// Student health insurance ID
            /// </summary>
            [JsonPropertyName("hi_id")]
            public string? HealthInsuranceID { get; set; } = null;

            /// <summary>
            /// Student health insurance expiration date
            /// </summary>
            [JsonPropertyName("hi_expire_date")]
            public DateTime HealthInsuranceExpirationDate { get; set; }

            /// <summary>
            /// Student specialization
            /// </summary>
            [JsonPropertyName("specialization")]
            public string? Specialization { get; set; } = null;

            /// <summary>
            /// Student main training program plan
            /// </summary>
            [JsonPropertyName("training_program_plan")]
            public string? TrainingProgramPlan { get; set; } = null;

            /// <summary>
            /// Student second training program plan (if available)
            /// </summary>
            [JsonPropertyName("training_program_plan_2")]
            public string? TrainingProgramPlan2 { get; set; } = null;

            /// <summary>
            /// Student email (school provided, ex. Microsoft, Google,...)
            /// </summary>
            [JsonPropertyName("school_email")]
            public string? SchoolEmail { get; set; } = null;

            /// <summary>
            /// Student email
            /// </summary>
            [JsonPropertyName("personal_email")]
            public string? PersonalEmail { get; set; } = null;

            /// <summary>
            /// Student phone number
            /// </summary>
            [JsonPropertyName("phone_number")]
            public string? PhoneNumber { get; set; }

            /// <summary>
            /// Student facebook link
            /// </summary>
            [JsonPropertyName("facebook_url")]
            public string? FacebookLink { get; set; } = null;

            /// <summary>
            /// Student class name (ex. 20R12)
            /// </summary>
            [JsonPropertyName("school_class")]
            public string? ClassName { get; set; } = null;
        }
    }
}
