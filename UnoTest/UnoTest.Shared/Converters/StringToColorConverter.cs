﻿using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Extentions;
using UnoTest.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Converters
{
    class StringToColorConverter :StatusConvertionBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => StatusToBrush((CorrectionStatus)((string)value).To<int>());

        public object ConvertBack(object value, Type targetType, object parameter, string language)=>throw new NotImplementedException();

    }
}
