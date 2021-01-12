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
    }
}
