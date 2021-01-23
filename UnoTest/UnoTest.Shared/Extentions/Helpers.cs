using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace UnoTest.Shared.Extentions
{
    public static class Helpers
    {
        public static SolidColorBrush FromHex(this string colorStr) => new SolidColorBrush(GetColor(colorStr));
        public static Color GetColor(this string colorStr)
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
