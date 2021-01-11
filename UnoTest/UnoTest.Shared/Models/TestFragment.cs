using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    public class TestFragment
    {
        public TestFragment()
        {
            CloseAnswers = new List<int>();
        }
        public int Number { get; set; }
        public int PreviousAnswer { get; set; }
        public List<int> CloseAnswers { get; set; }
    }
}
