using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DutWrapper.News
{
    public class NewsSubject : NewsGlobal
    {
        [JsonPropertyName("lecturer_name")]
        public string? LecturerName { get; set; }

        [JsonPropertyName("lecturer_gender")]
        public LecturerGender LecturerGender { get; set; } = LecturerGender.Unknown;

        [JsonPropertyName("affected_class")]
        public List<SubjectAffected> AffectedClass { get; set; } = new List<SubjectAffected>();

        [JsonPropertyName("status")]
        public SubjectStatus Status { get; set; } = SubjectStatus.Unknown;

        [JsonPropertyName("affected_date")]
        public long? DateAffected { get; set; }

        [JsonIgnore]
        public DateTimeOffset? DateTimeAffected
        {
            get { return DateAffected == null ? new DateTimeOffset?() : DateTimeOffset.FromUnixTimeMilliseconds((long)DateAffected); }
        }

        [JsonPropertyName("makeup_room")]
        public string? Room { get; set; }

        [JsonPropertyName("affected_lessons")]
        public Range? LessonAffected { get; set; }

        public new string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                IgnoreNullValues = false
            });
        }
    }
}
