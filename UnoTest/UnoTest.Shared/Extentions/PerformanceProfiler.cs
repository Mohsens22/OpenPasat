using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Olive;

namespace UnoTest.Extentions
{
    public static class PerformanceProfiler
    {
        static Stopwatch _watch = new Stopwatch();
        static List<KeyValuePair<string, long>> values = new List<KeyValuePair<string, long>>();

        static void Record(string message, long ellipsed)
        {
            values.Add(new KeyValuePair<string, long>(message, ellipsed));
        }

        public static void Begin()
        {
            _watch.Start();
        }
        public static void Record(string message)
        {
            Record(message, _watch.ElapsedMilliseconds);
            _watch.Restart();
        }
        public static string End()
        {
            var result = GetReport();
            Clear();
            return result;
        }
        public static string GetReport()=> values.Select(x => $"{x.Key} => {x.Value}ms").ToLinesString();
        private static void Clear()
        {
            values= new List<KeyValuePair<string, long>>();
            _watch.Stop();
            _watch.Reset();
        }
    }
}
