using Olive;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.Services
{
    public static class TestFactory
    {
        static Random _rnd = new Random();
        public static TestSheet Load(this TestIndentifier identifier)
        {
            var sheet = Load(identifier.TestCount);
            sheet.TestInfo = identifier;
            return sheet;
        }

        private static TestSheet Load(int count)
        {
#if DEBUG
            Debug.WriteLine("TestLoading...");
#endif
            var sheet = new TestSheet();

            int? prev = null;
            int? lastResult = null;
            for (int i = 0; i < count + 1; i++)
            {
                var fragment = new TestFragment();
                var num = _rnd.Next(1, 10);
                fragment.Number = num;
                if (prev.HasValue)
                {
                    var response = num + prev;
                    fragment.PreviousAnswer = response;
                    fragment.CloseAnswers=fragment.CloseAnswers.CreateArtifacts(response.Value, prev.Value, num, lastResult)
                        .Randomize()
                        .Where(x => x > 0 & x < 19)
                        .Except(response.Value)
                        .Distinct()
                        .Take(4)
                        .ToList()
                        ;

                    lastResult = response;
                }
                prev = num;
                sheet.TestFragments.Add(fragment);
            }

#if DEBUG
            Debug.WriteLine($"{sheet.TestFragments.Count} Test Loaded...");
#endif
            return sheet;
        }

        private static List<int> CreateArtifacts(this List<int> radomine, int response, int prev, int num, int? lastResult)
        {
            if (lastResult.HasValue)
            {
                radomine.Add(lastResult.Value + prev);
                radomine.Add(lastResult.Value + num);
                radomine.Add(response + lastResult.Value);
            }
            
            radomine.Add(response + 1);
            radomine.Add(response + 2);
            radomine.Add(response + 3);
            radomine.Add(response - 1);
            radomine.Add(response - 2);
            radomine.Add(response - 3);
            return radomine;

        }
    }
}
