using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Data;
using UnoTest.Models;

namespace UnoTest.Shared.Logic
{
    public static class TestManager
    {
        public static void InsetTest(TestIndentifier test)
        {
            var identifierRepo = GenericRepository.Of<TestIndentifier>();

            identifierRepo.Add(test);
            
        }
        public static List<TestIndentifier> GetTestsFor(User user)
        {
            var identifierRepo = GenericRepository.Of<TestIndentifier>();
            return identifierRepo.FindAll(x => x.UserId == user.Id).ToList();
        }

        public static TestIndentifier EagerLoad(TestIndentifier test)
        {
            var fragmentRepo = GenericRepository.Of<TestFragment>();
            var answerRepo = GenericRepository.Of<TestAnswer>();
            var userRepo = GenericRepository.Of<User>();

            test.Answers = answerRepo.FindAll(x => x.IndentifierId == test.Id).ToList();
            test.TestFragments = fragmentRepo.FindAll(x => x.IndentifierId == test.Id).ToList();
            foreach (var item in test.Answers)
            {
                item.TestFragment = test.TestFragments.FirstOrDefault(x => x.Id == item.TestFragmentId);
                item.PreFragment = test.TestFragments.FirstOrDefault(x => x.Id == item.PreFragmentId);
            }
            test.User = userRepo.Get(test.UserId);
            return test;
        }
    }
}
