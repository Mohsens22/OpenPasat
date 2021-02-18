using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Extentions;
using UnoTest.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UnoTest.Converters
{
    public class StatusToColorConverter : StatusConvertionBase, IValueConverter
    {

       

        public object Convert(object value, Type targetType, object parameter, string language) => StatusToBrush((CorrectionStatus)value);
        public object ConvertBack(object value, Type targetType, object parameter, string language)=> throw new NotImplementedException();

    }
}
