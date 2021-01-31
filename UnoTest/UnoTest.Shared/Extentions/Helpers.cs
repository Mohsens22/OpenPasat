using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Infrastructure.Features;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Reflection;
using Olive;
using System.Linq;

namespace UnoTest.Shared.Extentions
{
    public static class Helpers
    {
        public static IEnumerable<KeyValuePair<string, FeatureAvailability>> GetFeatures(this FeatureConfiguration config)
        {
            return config.AsDictionary().Select(x => new KeyValuePair<string, FeatureAvailability>(x.Key, (FeatureAvailability)x.Value));
        }

        public static T ToObject<T>(this IDictionary<string, object> source)
        where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

        public static bool IsAvailable(this FeatureAvailability availibility) => availibility == FeatureAvailability.Available;

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
        public static void CopyToClipboard(this string txt)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(txt);
            Clipboard.SetContent(dataPackage);
        }
    }
}
