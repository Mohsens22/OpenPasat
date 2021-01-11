using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    public class TestAnswer
    {
        public TestFragment TestFragment { get; set; }
        public int? Input { get; set; }
        public CorrectionStatus Status { get; set; }
        public DateTimeOffset RepresentationTime { get; set; }
        public DateTimeOffset InputTime { get; set; }
        public int InputSpeed { get; set; }
    }
}
