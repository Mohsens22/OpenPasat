﻿using System;
using Olive;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    class IntToStatusStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (int)(double)value;
            var color = "";
            switch (data)
            {
                case -1:
                    color = "";
                    break;
                case 1:
                    color = "";
                    break;
                default:
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
