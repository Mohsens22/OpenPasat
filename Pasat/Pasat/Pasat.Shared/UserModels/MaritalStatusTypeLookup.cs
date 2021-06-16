using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pasat.Models;

namespace Pasat.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class MaritalStatusTypeLookup
    {
        public MaritalStatusTypeLookup(MaritalStatus rp) => this.Item = rp;
        public MaritalStatus Item { get; set; }
        public string Display { get => Item.ToString(); }

        public static List<MaritalStatusTypeLookup> Load()
        {
            return ((IList<MaritalStatus>)Enum.GetValues(typeof(MaritalStatus)))
                         .Select(x => new MaritalStatusTypeLookup(x)).ToList();
        }
    }
}
