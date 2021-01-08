using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Services
{
    public static class TestItemService
    {
        public static TestItem LoadItems(int count)
        {
            var items = new TestItem();
            var rnd = new Random();
            int prev=0;
            for (int i = 0; i < count+1; i++)
            {
                var num = rnd.Next(0, 9);
                if (items.Items.Any())
                {
                    items.Results.Add(num + prev);
                }
                items.Items.Add(num);
                prev = num;
            }
            return items;
        }

        public static TestItem LoadItems() => LoadItems(60);
    }
}
