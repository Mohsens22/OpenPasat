using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pasat.Models
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestAnswer: BaseModel
    {
        public static TestAnswer NotAnswered(TestFragment fragment,TestFragment preFragment) => new TestAnswer 
        { 
            Status = CorrectionStatus.NoEntry,
            TestFragment=fragment,
            PreFragment=preFragment,
            InputType=InputType.None 
        };
        public static TestAnswer Answer(TestFragment fragment, TestFragment preFragment,int input,DateTimeOffset inTime,InputType inputType)
        {
            var answer = new TestAnswer 
            { 
                Input = input,
                TestFragment = fragment,
                PreFragment = preFragment,
                InputTime =inTime,
                InputType=inputType 
            };
            if (input==fragment.PreviousAnswer)
            {
                answer.Status = CorrectionStatus.True;
            }
            else
            {
                answer.Status = CorrectionStatus.False;
            }
            answer.InputSpeed = inTime.ToUnixTimeMilliseconds() - fragment.RepresentationTime.ToUnixTimeMilliseconds() ;

            return answer;
        }

        
        public int? Input { get; set; }
        public CorrectionStatus Status { get; set; }
        public DateTimeOffset InputTime { get; set; }
        public long? InputSpeed { get; set; }
        public InputType InputType { get; set; }
        public TestFragment TestFragment { get; set; }
        public int? TestFragmentId { get; set; }
        [JsonIgnore]
        public TestFragment PreFragment { get; set; }
        public int? PreFragmentId { get; set; }
        [JsonIgnore]
        public TestIndentifier Indentifier { get; set; }
        public int? IndentifierId { get; set; }

        public override string ToString() => $"{Id}-{Status} {InputSpeed} {InputType}";
    }
}
