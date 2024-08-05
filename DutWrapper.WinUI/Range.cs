using System.Text.Json.Serialization;

namespace DutWrapper.WinUI
{
    public class Range
    {
        [JsonPropertyName("start")]
        public int Start { get; private set; }

        [JsonPropertyName("end")]
        public int End { get; private set; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Start, End);
        }
    }
}
