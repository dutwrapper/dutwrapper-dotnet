using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper
{
    public static partial class Account
    {
        public class SubjectFee
        {
            #region Basic Information
            /// <summary>
            /// Subject ID.
            /// </summary>
            [JsonPropertyName("id")]
            public string? ID { get; set; } = null;

            /// <summary>
            /// Subject name.
            /// </summary>
            [JsonPropertyName("name")]
            public string? Name { get; set; } = null;

            /// <summary>
            /// Subject credit
            /// </summary>
            [JsonPropertyName("credit")]
            public float Credit { get; set; } = 0;
            #endregion

            #region Subject Information
            [JsonPropertyName("is_high_quality")]
            public bool IsHighQuality { get; set; } = false;
            #endregion

            #region Fee information
            [JsonPropertyName("price")]
            public double Price { get; set; } = 0.0;

            [JsonPropertyName("is_debt")]
            public bool IsDebt { get; set; } = false;

            [JsonPropertyName("is_restudy")]
            public bool IsReStudy { get; set; } = false;

            [JsonPropertyName("verified_payment_at")]
            public string? VerifiedPaymentAt { get; set; } = null;
            #endregion
        }
    }
}
