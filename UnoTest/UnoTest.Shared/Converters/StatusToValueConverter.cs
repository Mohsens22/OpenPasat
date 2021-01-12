using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UnoTest.Shared.Converters
{
    public class StatusToValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (CorrectionStatus)value;
            var color = FromHex("#0078D7");
            switch (data)
            {
                case CorrectionStatus.False:
                    color= FromHex("#FF4343");
                    break;
                case CorrectionStatus.True:
                    color = FromHex("#00CC6A");
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        private SolidColorBrush FromHex(string colorStr)=> new SolidColorBrush(GetColor(colorStr));
        private Color GetColor(string colorStr)
        {
            colorStr = colorStr.Replace("#", string.Empty);
            // from #RRGGBB string
            var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
            var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
            var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);
            //get the color
            return Color.FromArgb(255, r, g, b);
        }
    }
}
