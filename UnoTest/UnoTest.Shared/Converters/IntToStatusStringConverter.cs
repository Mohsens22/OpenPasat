using System;
using Olive;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.Converters
{
    class IntToStatusStringConverter : StatusConvertionBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => StatusToGlyph((CorrectionStatus)(int)(double)value);

        public object ConvertBack(object value, Type targetType, object parameter, string language)=> throw new NotImplementedException();
    }
}
