using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Extentions;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class IntToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = ((string)value).To<int>();
            var color = "#0078D7".FromHex();
            switch (data)
            {
                case -1:
                    color = "#FF4343".FromHex();
                    break;
                case 1:
                    color = "#00CC6A".FromHex();
                    break;
                default:
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
