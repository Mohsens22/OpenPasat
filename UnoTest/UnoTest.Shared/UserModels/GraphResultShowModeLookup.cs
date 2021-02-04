using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnoTest.Shared.UserModels
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
                items.Add(new GraphResultShowModeLookup(GraphResultShowMode.True, "Correct"));
            if (hasFalse)
                items.Add(new GraphResultShowModeLookup(GraphResultShowMode.False, "Incorrect"));
            if (hasFalse&hasTrue)
                items.Add(new GraphResultShowModeLookup(GraphResultShowMode.Mixed, "Mixed"));

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
