using Pasat.UserModels;
using System;
using System.Collections.Generic;
using System.Text;
using Olive;
using Windows.UI.Xaml;

namespace Pasat.Extentions
{
    public static class LanguageHelper
    {
        public static Language AppLanguage
        {
            get=> GetLanguage(GetString("Language", "Tag"));
            set => Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = GetTag(value);
        }
        public static string GetTag(Language lang) => lang.ToString();
        public static string GetTag() => GetTag(AppLanguage).ToLower();
        public static Language GetLanguage(string tag)
        {
            if (tag.StartsWith("en", false))
                return Language.En;

            if (tag.StartsWith("fa", false))
                return Language.Fa;

            return Language.En;
        }
        public static string GetString(string title, string Property)
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var expected = loader.GetString(title + "/" + Property);
            return expected;
        }

        public static FlowDirection GetObjectFlowDirection(string Title)
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var expected = loader.GetString(Title + "/FlowDirection");
            if (expected.StartsWith("R") || expected.StartsWith("r"))
                return FlowDirection.RightToLeft;
            else return FlowDirection.LeftToRight;
        }
    }
}
