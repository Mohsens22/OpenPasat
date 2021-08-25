using System;
using System.Collections.Generic;
using System.Text;
using Pasat.Models;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class GenderConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var gender = (Gender)value;
            switch (gender)
            {
                case Gender.NoAnswer:
                    return "";
                case Gender.Female:
                    return "F";
                case Gender.Male:
                    return "M";
                case Gender.NonBinary:
                    return "NB";
                case Gender.Transgender:
                    return "TG";
                case Gender.Other:
                    return "O";
                default:
                    return "";
                    
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
