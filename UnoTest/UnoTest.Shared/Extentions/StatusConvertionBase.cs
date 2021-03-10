using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Extentions;
using UnoTest.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UnoTest.Converters
{
    public abstract class StatusConvertionBase
    {
        protected SolidColorBrush trueColor = (SolidColorBrush)Application.Current.Resources["CorrectAnswerBrush"];
        protected SolidColorBrush noColor = (SolidColorBrush)Application.Current.Resources["NoAnswerBrush"];
        protected SolidColorBrush falseColor = (SolidColorBrush)Application.Current.Resources["FalseAnswerBrush"];
        protected SolidColorBrush transparent = new SolidColorBrush(Colors.Transparent);

        protected string trueGlyph = "";
        protected string falseGlyph = "";
        protected string noGlyph = "";


        protected SolidColorBrush StatusToBrush(CorrectionStatus status)
        {
            var color = noColor;
            switch (status)
            {
                case CorrectionStatus.False:
                    color = falseColor;
                    break;
                case CorrectionStatus.True:
                    color = trueColor;
                    break;
            }

            return color;
        }

        protected string StatusToGlyph(CorrectionStatus status)
        {
            var glyph = "";
            switch (status)
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
    }
}
