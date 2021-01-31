﻿using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    class StringToGlyphConverter :StatusConvertionBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => StatusToGlyph((CorrectionStatus)((string)value).To<int>());

        public object ConvertBack(object value, Type targetType, object parameter, string language)=>throw new NotImplementedException();
    }
}
