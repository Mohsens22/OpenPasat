using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class TestSheetToTrueReactionTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (TestSheet)value;
            var trueCount = data.Answers.Where(x => x.Status == CorrectionStatus.True).Select(x => x.InputSpeed);
            return ((int)trueCount.Average()) + " ms"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
