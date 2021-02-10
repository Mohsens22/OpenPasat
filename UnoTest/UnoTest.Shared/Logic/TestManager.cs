using System;
using System.Collections.Generic;
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
    }
}
