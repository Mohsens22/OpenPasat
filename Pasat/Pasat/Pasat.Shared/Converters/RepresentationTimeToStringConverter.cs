using Pasat.Models;
using Pasat.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class RepresentationTimeToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var yp = (RepresentationType)value;
            return RepresentationTypeLookup.Load().FirstOrDefault(x => x.Item == yp).Display;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
