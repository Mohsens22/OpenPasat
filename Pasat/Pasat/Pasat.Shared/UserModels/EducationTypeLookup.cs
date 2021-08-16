using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pasat.Extentions;
using Pasat.Models;

namespace Pasat.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class EducationTypeLookup
    {
        public EducationTypeLookup(Education rp,string display)
        {
            this.Item = rp;
            this.Display = display;
        }
        public Education Item { get; set; }
        public string Display { get; set; }

        public static List<EducationTypeLookup> Load()
        {
            return new List<EducationTypeLookup>
            {
                new EducationTypeLookup(Education.NoAnswer,LanguageHelper.GetString("NoAnswer","Text")),
                new EducationTypeLookup(Education.Illiterate,LanguageHelper.GetString("Illiterate","Text")),
                new EducationTypeLookup(Education.UnofficialEducation,LanguageHelper.GetString("UnofficialEducation","Text")),
                new EducationTypeLookup(Education.Elementary,LanguageHelper.GetString("Elementary","Text")),
                new EducationTypeLookup(Education.JuniorHigh,LanguageHelper.GetString("JuniorHigh","Text")),
                new EducationTypeLookup(Education.Diploma,LanguageHelper.GetString("Diploma","Text")),
                new EducationTypeLookup(Education.Associates,LanguageHelper.GetString("Associates","Text")),
                new EducationTypeLookup(Education.Bachelors,LanguageHelper.GetString("Bachelors","Text")),
                new EducationTypeLookup(Education.Masters,LanguageHelper.GetString("Masters","Text")),
                new EducationTypeLookup(Education.Doctorate,LanguageHelper.GetString("UDoctorate","Text")),
                new EducationTypeLookup(Education.UppderDoctorate,LanguageHelper.GetString("NoAnswer","Text")),
                new EducationTypeLookup(Education.Other,LanguageHelper.GetString("Other","Text")),
            };
        }
    }
}
