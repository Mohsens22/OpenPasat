using System;
using System.Collections.Generic;
using System.Text;

namespace Pasat.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class LanguageLookup
    {
        public LanguageLookup(Language rp, string display)
        {
            this.Item = rp;
            this.Display = display;
        }
        public Language Item { get; set; }
        public string Display { get; set; }

        public static List<LanguageLookup> Load()
        {
            return new List<LanguageLookup>
            {
                new LanguageLookup(Language.En,"English"),
                new LanguageLookup(Language.Fa,"فارسی")
            };
        }
    }
    public enum Language
    {
        En,
        Fa
    }
}
