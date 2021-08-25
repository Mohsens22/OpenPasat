using Pasat.Extentions;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class BoolToValidationConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isValid = (bool)value;
            if (isValid)
            {
                return LanguageHelper.GetString("Valid", "Text");
            }
            else
            {
                return LanguageHelper.GetString("Invalid", "Text");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
