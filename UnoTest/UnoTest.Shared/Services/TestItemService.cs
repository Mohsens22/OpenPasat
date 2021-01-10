using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.Services
{
    public static class TestItemService
    {
        static Random _rnd = new Random();
        public static TestSheet LoadItems(int count=60)
        {
            var items = new TestSheet();

            int prev = 0;
            for (int i = 0; i < count + 1; i++)
            {
                var radomine = new List<int>();
                var num = _rnd.Next(1, 10);
                if (items.Items.Any())
                {
                    var response = num + prev;
                    radomine = CreateArtifacts(radomine, response, prev, num, items.Results.LastOrDefault())
                        .Randomize()
                        .Where(x => x > 0 & x < 19)
                        .Except(response)
                        .Distinct()
                        .Take(4)
                        .ToList()
                        ;
                    items.Results.Add(response);
                    items.CloseAnswers.Add(radomine);
                }
                items.Items.Add(num);
                prev = num;
            }
            return items;
        }

        private static List<int> CreateArtifacts(List<int> radomine, int response, int prev, int num, int lastResult)
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
