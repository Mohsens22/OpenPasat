using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestFragment: BaseModel
    {
        public int Number { get; set; }
        public int? PreviousAnswer { get; set; }
        public string CloseAnswers { get; set; }
        public DateTimeOffset RepresentationTime { get; set; }
        public override string ToString() => $"{Id}Number:{Number},Answer:{PreviousAnswer}";
    }
}
