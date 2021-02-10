using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UnoTest.Models
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestFragment: BaseModel
    {
        public int Number { get; set; }
        public int? PreviousAnswer { get; set; }
        public string CloseAnswers { get; set; }
        public DateTimeOffset RepresentationTime { get; set; }
        [JsonIgnore]
        public TestIndentifier Indentifier { get; set; }
        public int? IndentifierId { get; set; }
        [JsonIgnore]
        public TestAnswer PreFragmentOf { get; set; }
        [JsonIgnore]
        public TestAnswer FragmentOf { get; set; }

        public override string ToString() => $"{Id}Number:{Number},Answer:{PreviousAnswer}";
    }
}
