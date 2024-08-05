using DutWrapper.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DutWrapper.WinUI.News
{
    public class SubjectAffected
    {
        [JsonPropertyName("code_list")]
        public List<SubjectCode> CodeList { get; private set; }

        [JsonPropertyName("name")]
        public string SubjectName { get; private set; }

        public SubjectAffected(string subjectName)
        {
            this.SubjectName = subjectName;
            this.CodeList = new List<SubjectCode>();
        }

        public SubjectAffected(List<SubjectCode> codeList, string subjectName)
        {
            this.CodeList = codeList;
            this.SubjectName = subjectName;
        }
    }
}
