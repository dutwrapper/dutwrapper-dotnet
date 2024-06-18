using System;

namespace DutWrapper.Model.Account
{
    public class AccountInformation
    {
        /// <summary>
        /// Student name
        /// </summary>
        public string? Name { get; set; } = null;

        /// <summary>
        /// Student account ID
        /// </summary>
        public string? ID { get; set; } = null;

        /// <summary>
        /// Student date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Student date place
        /// </summary>
        public string? DatePlace { get; set; }

        /// <summary>
        /// Student gender
        /// </summary>
        public LecturerGender Gender { get; set; } = LecturerGender.Unknown;

        /// <summary>
        /// Student ethnicity
        /// </summary>
        public string? Ethnicity { get; set; } = null;

        /// <summary>
        /// Student nationality
        /// </summary>
        public string? Nationality { get; set; } = null;

        /// <summary>
        /// Student religion
        /// </summary>
        public string? Religion { get; set; } = null;

        /// <summary>
        /// Student national card ID
        /// </summary>
        public string? NationalCardID { get; set; } = null;

        /// <summary>
        /// Student national card issue date
        /// </summary>
        public DateTime NationalCardIssueDate { get; set; }

        /// <summary>
        /// Student national card issue place
        /// </summary>
        public string? NationalCardIssuePlace { get; set; } = null;

        /// <summary>
        /// Student citizen card ID (replace for national card)
        /// </summary>
        public string? CitizenCardID { get; set; } = null;

        /// <summary>
        /// Student citizen card issue date.
        /// </summary>
        public DateTime CitizenCardIssueDate { get; set; }

        /// <summary>
        /// Student bank information
        /// </summary>
        public string? BankInfo { get; set; } = null;

        /// <summary>
        /// Student health insurance ID
        /// </summary>
        public string? HealthInsuranceID { get; set; } = null;

        /// <summary>
        /// Student health insurance expiration date
        /// </summary>
        public DateTime HealthInsuranceExpirationDate { get; set; }

        /// <summary>
        /// Student specialization
        /// </summary>
        public string? Specialization { get; set; } = null;

        /// <summary>
        /// Student main training program plan
        /// </summary>
        public string? TrainingProgramPlan { get; set; } = null;

        /// <summary>
        /// Student second training program plan (if available)
        /// </summary>
        public string? TrainingProgramPlan2 { get; set; } = null;

        /// <summary>
        /// Student email (school provided, ex. Microsoft, Google,...)
        /// </summary>
        public string? SchoolEmail { get; set; } = null;

        /// <summary>
        /// Student email
        /// </summary>
        public string? PersonalEmail { get; set; } = null;

        /// <summary>
        /// Student phone number
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Student facebook link
        /// </summary>
        public string? FacebookLink { get; set; } = null;

        /// <summary>
        /// Student class name (ex. 20R12)
        /// </summary>
        public string? ClassName { get; set; } = null;
    }
}
