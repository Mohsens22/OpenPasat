using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace UnoTest.Converters
{
    public class DateTimeOffsetToAgeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var offset = (DateTimeOffset?)value;
            if (offset.HasValue)
            {
                return DateTimeOffset.Now.Year - offset.Value.Year;
            }
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
