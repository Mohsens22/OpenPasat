using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class RepresentationTypeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new RepresentationTypeLookup((RepresentationType)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value as RepresentationTypeLookup).Item;
        }
    }
    public class RepresentationTypeLoader : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((IList<RepresentationType>)Enum.GetValues(typeof(RepresentationType)))
                          .Select(x => new RepresentationTypeLookup(x));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value as RepresentationTypeLookup).Item;
        }
    }
    class RepresentationTypeLookup
    {
        public RepresentationTypeLookup(RepresentationType rp) => this.Item = rp;
        public RepresentationType Item { get; set; }
        public string Display { get => Item.ToString(); }

    }
}
