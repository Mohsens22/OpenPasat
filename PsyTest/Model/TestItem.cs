using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyTest.Model
{
    public class TestItem
    {
        public TestItem()
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
        public List<KeyValuePair<bool,DateTimeOffset>> ResultsTaken { get; set; }
    }
}
