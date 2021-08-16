using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pasat.Extentions;
using Pasat.Models;

namespace Pasat.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class GenderTypeLookup
    {
        public GenderTypeLookup(Gender rp,string display)
        {
            this.Item = rp;
            this.Display = display;
        }
        public Gender Item { get; set; }
        public string Display { get; set; }

        public static List<GenderTypeLookup> Load()
        {
            return new List<GenderTypeLookup>
            {
                new GenderTypeLookup(Gender.NoAnswer,LanguageHelper.GetString("NoAnswer","Text")),
                new GenderTypeLookup(Gender.Female,LanguageHelper.GetString("Female","Text")),
                new GenderTypeLookup(Gender.Male,LanguageHelper.GetString("Male","Text")),
                new GenderTypeLookup(Gender.NonBinary,LanguageHelper.GetString("NonBinary","Text")),
                new GenderTypeLookup(Gender.Transgender,LanguageHelper.GetString("Transgender","Text")),
                new GenderTypeLookup(Gender.Other,LanguageHelper.GetString("Other","Text"))
            };
        }
    }
}
