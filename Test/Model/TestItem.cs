using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class TestItem
    {
        public TestItem()
        {
            Items = new List<int>();
            Results = new List<int>();
        }
        public List<int> Items { get; set; }
        public List<int> Results { get; set; }
    }
}
