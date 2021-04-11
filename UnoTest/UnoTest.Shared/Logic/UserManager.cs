using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Data;
using Olive;
using UnoTest.Models;

namespace UnoTest.Logic
{
    public static class UserManager
    {
        public static List<User> GetUsers(int index, int size)
        {
            using (var context = new Context())
            {
                var items = context.Set<User>()
                    .Skip(index * size)
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
        public static List<User> GetUsers(string searchTerm, int size=5)
        {
            var items = new List<User>();

            if (searchTerm.IsEmpty())
            {
                return items;
            }
            if (searchTerm.Length<3)
            {
                return items;
            }
            using (var context = new Context())
            {
                
                if (searchTerm.StartsWith("@"))
                {
                    items = context.Users
                        .Where(u => u.Username.Contains(searchTerm.ToLower()))
                        .ToList();

                }
                else
                {
                    items = context.Users
                        .Where(u => u.Username.Contains(searchTerm.ToLower())||u.FullName.Contains(searchTerm))
                        .ToList();
                }
                return items;
            }
        }

        public static User GetDefaultUser() => GenericRepository.Of<User>().Get(1);
        public static User Get(int? id)
        {
            using (var context = new Context())
            {
                var user = context.Set<User>().Find(id);
                user.TestCount = context.Entry(user)
                        .Collection(x => x.Tests)
                        .Query()
                        .Count();

                return user;
            }
        }

        public static void AddUser(User user)
        {
            var repo = GenericRepository.Of<User>();
            user.Username = user.Username.ToLower();
            repo.Add(user);
            ReactiveFactory.ChangeUserList();
        }
        public static bool UserExists(string username)
        {
            if (username.IsEmpty())
            {
                return false;
            }
            var repo = GenericRepository.Of<User>();
            username = username.ToLower();
            return repo.Exists(x => x.Username == username);
        }
    }
}
