using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class DateFormatConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var offset = (DateTimeOffset)value;
            return offset.ToLocalTime().ToString("dddd, dd MMMM yyyy HH:mm:ss");

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
