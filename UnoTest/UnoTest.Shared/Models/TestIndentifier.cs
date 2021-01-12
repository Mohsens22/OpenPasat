using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    public class TestIndentifier
    {
        public int TestCount { get; set; }
        public int Quantum { get; set; }
        public int ImpulseRate { get; set; }
        public RepresentationType RepresentationType { get; set; }
        public bool Correction { get; set; }

        public int AnswerTime { get => Quantum - ImpulseRate; }
        public bool IsVisualEnabled { get => RepresentationType != RepresentationType.Audio; }
        public bool IsAudioEnabled { get => RepresentationType != RepresentationType.UI; }
        public override string ToString() => $"Count:{TestCount},Time:{Quantum}.{ImpulseRate}, Mode:{RepresentationType}";
    }
}
