using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Data;
using UnoTest.Extentions;
using UnoTest.Models;

namespace UnoTest.Logic
{
    public static class TestManager
    {
        public static async Task InsetTest(TestIndentifier test)
        {
            if (App.Features.InAppDatabase == Infrastructure.Features.FeatureAvailability.Available)
            {
                var identifierRepo = GenericRepository.Of<TestIndentifier>();

                await identifierRepo.AddAsync(test);
                ReactiveFactory.ChangeUser(new User { Id=test.UserId});
            }

           


        }
        public static List<TestIndentifier> GetTestsFor(User user)
        {
            var identifierRepo = GenericRepository.Of<TestIndentifier>();
            return identifierRepo.FindAll(x => x.UserId == user.Id).ToList();
        }

        public static async Task<TestIndentifier> EagerLoad(TestIndentifier test)
        {
            var fragmentRepo = GenericRepository.Of<TestFragment>();
            var answerRepo = GenericRepository.Of<TestAnswer>();
            var userRepo = GenericRepository.Of<User>();

            test.Answers = (await answerRepo.FindAllAsync(x => x.IndentifierId == test.Id)).ToList();
            test.TestFragments = (await fragmentRepo.FindAllAsync(x => x.IndentifierId == test.Id)).ToList();
            foreach (var item in test.Answers)
            {
                item.TestFragment = test.TestFragments.FirstOrDefault(x => x.Id == item.TestFragmentId);
                item.PreFragment = test.TestFragments.FirstOrDefault(x => x.Id == item.PreFragmentId);
            }
            test.User = await userRepo.GetAsync(test.UserId);
            return test;
        }
    }
}
