﻿using System;
using System.Collections.Generic;
using System.Text;
using Pasat.Models;
using Windows.System;
using Olive;
using System.Linq;

namespace Pasat.Logic
{
    public static class ValidationFactory
    {
        public static ValidationContext LoadValidation(this TestIndentifier identifier)
        {
            var context = new ValidationContext();

            var possibleAnswers = new List<VirtualKey> { VirtualKey.Up, VirtualKey.Down, VirtualKey.Left, VirtualKey.Right };
            possibleAnswers.Add(possibleAnswers.PickRandom());
            possibleAnswers.Add(possibleAnswers.PickRandom());
            context.Items = possibleAnswers.Randomize().Select(x=>new ValidationItem { Key = x }).ToList();

            


            return context;
        }
    }
}
