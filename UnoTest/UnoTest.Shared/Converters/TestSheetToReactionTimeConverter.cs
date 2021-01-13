using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Models;
using Windows.UI.Xaml.Data;

namespace UnoTest.Shared.Converters
{
    public class TestSheetToReactionTimeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var data = (TestSheet)value;
            var answered = data.Answers.Where(x => x.Status != CorrectionStatus.NoEntry).Select(x => x.InputSpeed);
            return ((int)answered.Average().Value) +" ms";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
