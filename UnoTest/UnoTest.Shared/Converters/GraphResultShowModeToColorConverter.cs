using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Extentions;
using UnoTest.Shared.UserModels;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class GraphResultShowModeToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (GraphResultShowMode)value;
            var color = "#0078D7".FromHex();
            switch (data)
            {
                case GraphResultShowMode.False:
                    color = "#FF4343".FromHex();
                    break;
                case GraphResultShowMode.True:
                    color = "#00CC6A".FromHex();
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
