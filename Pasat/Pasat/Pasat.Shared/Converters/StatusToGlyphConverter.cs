using System;
using System.Collections.Generic;
using System.Text;
using Pasat.Models;
using Windows.UI.Xaml.Data;

namespace Pasat.Converters
{
    public class StatusToGlyphConverter: StatusConvertionBase,IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => StatusToGlyph((CorrectionStatus)value);

        public object ConvertBack(object value, Type targetType, object parameter, string language)=>throw new NotImplementedException();
    }
}
