using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class StatusToGlyphConverter: StatusConvertionBase,IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => StatusToGlyph((CorrectionStatus)value);

        public object ConvertBack(object value, Type targetType, object parameter, string language)=>throw new NotImplementedException();
    }
}
