using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    class StringToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (CorrectionStatus)((string)value).To<int>();
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
