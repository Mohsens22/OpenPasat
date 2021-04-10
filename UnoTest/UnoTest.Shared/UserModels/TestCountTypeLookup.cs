using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.UserModels
{
    public class TestCountTypeLookup
    {
        public TestCount Item { get; set; }
        public string Display { get; set; }
        public static List<TestCountTypeLookup> Load()
        {
            var list = new List<TestCountTypeLookup>();
#if DEBUG
            list.Add(new TestCountTypeLookup
            {
                Display = "1",
                Item= TestCount.DevOne
            }) ;
            list.Add(new TestCountTypeLookup
            {
                Display = "5",
                Item = TestCount.DevFive
            });
            list.Add(new TestCountTypeLookup
            {
                Display = "10",
                Item = TestCount.DevTen
            });
#endif
            list.Add(new TestCountTypeLookup
            {
                Display = "15",
                Item = TestCount.Fifteen
            });
            list.Add(new TestCountTypeLookup
            {
                Display = "30",
                Item = TestCount.Thirty
            });
            list.Add(new TestCountTypeLookup
            {
                Display = "60",
                Item = TestCount.Sixty
            });
            list.Add(new TestCountTypeLookup
            {
                Display = "120",
                Item = TestCount.HTwenty
            });

            list.Add(new TestCountTypeLookup
            {
                Display = "240",
                Item = TestCount.DevTwoHFourty
            });
            list.Add(new TestCountTypeLookup
            {
                Display = "480",
                Item = TestCount.DevFourHeighty
            });

            return list;
        }
    }
    public enum TestCount
    {
        DevOne=1,
        DevFive=5,
        DevTen=10,
        Fifteen=15,
        Thirty=30,
        Sixty=60,
        HTwenty=120,
        DevTwoHFourty=240,
        DevFourHeighty=480,
    }
}
