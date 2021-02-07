using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
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



    }
}
