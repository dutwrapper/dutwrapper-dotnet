using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.News
{
    public class SubjectAffected
    {
        [JsonPropertyName("code_list")]
        public List<SubjectCode> CodeList { get; private set; }

        [JsonPropertyName("name")]
        public string SubjectName { get; private set; }

        public SubjectAffected(string subjectName)
        {
            SubjectName = subjectName;
            CodeList = new List<SubjectCode>();
        }

        public SubjectAffected(List<SubjectCode> codeList, string subjectName)
        {
            CodeList = codeList;
            SubjectName = subjectName;
        }
    }
}
