using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnoTest.Shared.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class GraphResultShowModeLookup
    {
        public GraphResultShowModeLookup(GraphResultShowMode rp) => this.Item = rp;
        public GraphResultShowMode Item { get; set; }
        public string Display { get => Item.ToString(); }

        public static List<GraphResultShowModeLookup> Load()
        {
            return ((IList<GraphResultShowMode>)Enum.GetValues(typeof(GraphResultShowMode)))
                         .Select(x => new GraphResultShowModeLookup(x)).ToList();
        }
    }
    public enum GraphResultShowMode
    {
        Mixed,
        True,
        False
    }
}
