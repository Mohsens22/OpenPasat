using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Models;

namespace UnoTest.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class GenderTypeLookup
    {
        public GenderTypeLookup(Gender rp) => this.Item = rp;
        public Gender Item { get; set; }
        public string Display { get => Item.ToString(); }

        public static List<GenderTypeLookup> Load()
        {
            return ((IList<Gender>)Enum.GetValues(typeof(Gender)))
                         .Select(x => new GenderTypeLookup(x)).ToList();
        }
    }
}
