using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class RepresentationTypeLookup
    {
        public RepresentationTypeLookup(RepresentationType rp) => this.Item = rp;
        public RepresentationType Item { get; set; }
        public string Display { get => Item.ToString(); }

        public static List<RepresentationTypeLookup> Load()
        {
            return ((IList<RepresentationType>)Enum.GetValues(typeof(RepresentationType)))
                         .Select(x => new RepresentationTypeLookup(x)).ToList();
        }

    }
}
