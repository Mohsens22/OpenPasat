using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Models;

namespace UnoTest.UserModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class EducationTypeLookup
    {
        public EducationTypeLookup(Education rp) => this.Item = rp;
        public Education Item { get; set; }
        public string Display { get => Item.ToString(); }

        public static List<EducationTypeLookup> Load()
        {
            return ((IList<Education>)Enum.GetValues(typeof(Education)))
                         .Select(x => new EducationTypeLookup(x)).ToList();
        }
    }
}
