using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Data;
using UnoTest.Models;

namespace UnoTest.Shared.Logic
{
    public static class UserManager
    {
        public static List<User> GetUsers(int index, int size)
        {
            using (var context = new Context())
            {
                var items = context.Set<User>()
                    .Skip(index*size)
                    .Take(size)
                    .ToList();

                foreach (var item in items)
                {
                    item.TestCount = context.Entry(item)
                        .Collection(x => x.Tests)
                        .Query()
                        .Count();
                }
                return items;
            }
                
        }
    }
}
