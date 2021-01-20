﻿using System;
using System.Collections.Generic;

namespace UnoTest.Shared.Models
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestSheet
    {

        public TestSheet()
        {
            TestFragments = new List<TestFragment>();
            Answers = new List<TestAnswer>();
        }



        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public List<TestFragment> TestFragments { get; set; }
        public List<TestAnswer> Answers { get; set; }
        public TestIndentifier TestInfo { get; set; }

        public override string ToString() => "TestSheet";
    }
}
