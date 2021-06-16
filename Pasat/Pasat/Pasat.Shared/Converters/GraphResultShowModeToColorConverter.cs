using System;
using System.Collections.Generic;
using System.Text;
using Pasat.Extentions;
using Pasat.UserModels;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class GraphResultShowModeToColorConverter:StatusConvertionBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return noColor;

            var data = (GraphResultShowMode)value;
            switch (data)
            {
                case GraphResultShowMode.False:
                    return falseColor;
                    break;
                case GraphResultShowMode.True:
                    return trueColor;
                    break;
                default:
                    return noColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
