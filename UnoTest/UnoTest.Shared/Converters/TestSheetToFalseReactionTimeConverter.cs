using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class TestSheetToFalseReactionTimeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (TestSheet)value;
            var falseSpeeds = data.Answers.Where(x => x.Status == CorrectionStatus.False).Select(x => x.InputSpeed);
            return ((int)falseSpeeds.Average()) + " ms";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
