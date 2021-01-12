using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    public class TestAnswer
    {
        public static TestAnswer NotAnswered(TestFragment fragment) => new TestAnswer { Status = CorrectionStatus.NoEntry,TestFragment=fragment,InputType=InputType.None };
        public static TestAnswer Answer(TestFragment fragment,int input,DateTimeOffset inTime,InputType inputType)
        {
            var answer = new TestAnswer { Input = input,TestFragment = fragment,InputTime=inTime,InputType=inputType };
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

        public TestFragment TestFragment { get; set; }
        public int? Input { get; set; }
        public CorrectionStatus Status { get; set; }
        public DateTimeOffset InputTime { get; set; }
        public long? InputSpeed { get; set; }
        public InputType InputType { get; set; }

        public override string ToString() => $"{Status} {InputSpeed} {InputType}";
    }
}
