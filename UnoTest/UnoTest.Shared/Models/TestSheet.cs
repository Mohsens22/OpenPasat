using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    public class TestSheet
    {

        public TestSheet()
        {
            Items = new List<int>();
            Results = new List<int>();
            CloseAnswers = new List<List<int>>();
            ResultsTaken = new List<KeyValuePair<bool, DateTimeOffset>>();
            ItemsRepresented = new List<DateTimeOffset>();
        }
        public List<int> Items { get; set; }
        public List<int> Results { get; set; }
        public List<List<int>> CloseAnswers { get; set; }

        public List<DateTimeOffset> ItemsRepresented { get; set; }
        public List<KeyValuePair<bool, DateTimeOffset>> ResultsTaken { get; set; }
    }
}
