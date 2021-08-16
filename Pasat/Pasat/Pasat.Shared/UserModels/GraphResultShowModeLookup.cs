using Pasat.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pasat.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class GraphResultShowModeLookup
    {
        public GraphResultShowModeLookup(GraphResultShowMode rp, string display) 
        {
            this.Item = rp;
            this.Display = display;
        }
        public GraphResultShowMode Item { get; set; }
        public string Display { get; set; }

        public static List<GraphResultShowModeLookup> Load(bool hasTrue, bool hasFalse)
        {
            var items = new List<GraphResultShowModeLookup>();
            if (hasTrue)
                items.Add(new GraphResultShowModeLookup(GraphResultShowMode.True, LanguageHelper.GetString("Correct", "Text")));
            if (hasFalse)
                items.Add(new GraphResultShowModeLookup(GraphResultShowMode.False, LanguageHelper.GetString("False", "Text")));
            if (hasFalse&hasTrue)
                items.Add(new GraphResultShowModeLookup(GraphResultShowMode.Mixed, LanguageHelper.GetString("MixedType","Text")));

            return items;
        }
    }
    public enum GraphResultShowMode
    {
        True,
        Mixed,
        False
    }
}
