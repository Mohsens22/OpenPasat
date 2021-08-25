using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pasat.Models
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestIndentifier : BaseModel
    {
        public int TestCount { get; set; }
        public int Quantum { get; set; }
        public int ImpulseRate { get; set; }
        public RepresentationType RepresentationType { get; set; }
        public bool Correction { get; set; }

        public int AnswerTime { get => Quantum - ImpulseRate; }
        public bool IsVisualEnabled { get => RepresentationType != RepresentationType.Audio; }
        public bool IsAudioEnabled { get => RepresentationType != RepresentationType.UI; }
        public override string ToString() => $"{Id}-Count:{TestCount},Time:{Quantum}.{ImpulseRate}, Mode:{RepresentationType}";

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public List<TestFragment> TestFragments { get; set; }
        public List<TestAnswer> Answers { get; set; }

        public string ValidationString { get; set; }
        [JsonIgnore]
        public ValidationContext ValidationContext
        {
            get => ValidationContext.FromJson(this.ValidationString);
            set => ValidationString = ValidationContext.ToJson(value);
        }


        public int? UserId { get; set; }
        public User User { get; set; }

        public TestIndentifier Clone()
        {
            return new TestIndentifier
            {
                TestCount=this.TestCount,
                Quantum=this.Quantum,
                ImpulseRate=this.ImpulseRate,
                RepresentationType=this.RepresentationType,
                Correction=this.Correction,
                StartTime=this.StartTime,
                EndTime=this.EndTime,
                TestFragments=this.TestFragments,
                Answers=this.Answers,
                ValidationString=this.ValidationString,
                UserId=this.UserId


            };
        }


    }
}
