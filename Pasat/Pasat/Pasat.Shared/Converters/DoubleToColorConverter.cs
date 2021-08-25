using System;
using System.Collections.Generic;
using System.Text;
using Pasat.Extentions;
using Pasat.Models;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class DoubleToColorConverter :StatusConvertionBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => StatusToBrush((CorrectionStatus)((int)(double)value));

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
