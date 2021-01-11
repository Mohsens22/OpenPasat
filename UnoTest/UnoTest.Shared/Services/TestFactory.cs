using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.Services
{
    public static class TestFactory
    {
        static Random _rnd = new Random();
        public static TestSheet LoadItems(int count=60)
        {
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
                    fragment.PreviousAnswer = response.Value;
                    fragment.CloseAnswers=fragment.CloseAnswers.CreateArtifacts(response.Value, prev.Value, num, lastResult.Value)
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
            return sheet;
        }

        private static List<int> CreateArtifacts(this List<int> radomine, int response, int prev, int num, int lastResult)
        {
            radomine.Add(lastResult + prev);
            radomine.Add(lastResult + num);
            radomine.Add(response + lastResult);
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
