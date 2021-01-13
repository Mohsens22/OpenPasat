using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class TestSheetToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var input = (TestSheet)value;
            var point = input.Answers.Where(x => x.Status == CorrectionStatus.True).Count() - input.Answers.Where(x => x.Status == CorrectionStatus.False).Count();
            var all = input.Answers.Count;


            return $"{(point * 100) / all}%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
