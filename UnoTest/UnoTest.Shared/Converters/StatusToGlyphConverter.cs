using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class CorrectionStatusToGlyphConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (CorrectionStatus)value;
            var glyph = "";
            switch (data)
            {
                case CorrectionStatus.False:
                    glyph = "";
                    break;
                case CorrectionStatus.True:
                    glyph = "";
                    break;
            }

            return glyph;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
