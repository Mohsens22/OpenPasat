using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Extentions;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (CorrectionStatus)((string)value).To<int>();
            var color = "#0078D7".FromHex();
            switch (data)
            {
                case CorrectionStatus.False:
                    color = "#FF4343".FromHex();
                    break;
                case CorrectionStatus.True:
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
