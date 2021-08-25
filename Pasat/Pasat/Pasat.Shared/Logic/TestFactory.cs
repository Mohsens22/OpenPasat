using Olive;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Pasat.Models;

namespace Pasat.Logic
{
    public static class TestFactory
    {
        static Random _rnd = new Random();
        public static void Load(this TestIndentifier identifier)
        {
            sheet = identifier;
            identifier.Answers = new List<TestAnswer>();
            identifier.TestFragments = new List<TestFragment>();
            Load(identifier.TestCount);
            
        }
        static TestIndentifier sheet ;
        private static void Load(int count)
        {
#if DEBUG
            Debug.WriteLine("TestLoading...");
#endif

            int? prev = null;
            int? lastResult = null;
            for (int i = 0; i < count + 1; i++)
            {
                var fragment = new TestFragment();
                var num = getRandNum();
                fragment.Number = num;
                if (prev.HasValue)
                {
                    var response = num + prev;
                    fragment.PreviousAnswer = response;
                    
                    fragment.CloseAnswers= CreateArtifacts(response.Value, prev.Value, num, lastResult);

                    lastResult = response;
                }
                prev = num;
                sheet.TestFragments.Add(fragment);
            }

#if DEBUG
            Debug.WriteLine($"{sheet.TestFragments.Count} Test Loaded...");
#endif
        }

        private static int getRandNum()
        {
            var num = _rnd.Next(1, 10);
            if (sheet.TestFragments.Any())
            {
                while (sheet.TestFragments.TakeLast(3).Any(x => x.Number == num))
                {
                    num= _rnd.Next(1, 10);
                }
            }
            return num;
            
        }

        private static string CreateArtifacts(int response, int prev, int num, int? lastResult)
        {
            var radomine = new List<int>();

            if (lastResult.HasValue)
            {
                radomine.Add(lastResult.Value + prev);
                radomine.Add(lastResult.Value + num);
                radomine.Add(response + lastResult.Value);
            }
            
            radomine.Add(response + 1);
            radomine.Add(response + 2);
            radomine.Add(response + 3);
            radomine.Add(response + 4);
            radomine.Add(response - 1);
            radomine.Add(response - 2);
            radomine.Add(response - 3);
            radomine.Add(response - 4);
            return radomine.Randomize()
                        .Where(x => x > 0 & x < 19)
                        .Except(response)
                        .Distinct()
                        .Take(4).ToString(" ");

        }
    }
}
