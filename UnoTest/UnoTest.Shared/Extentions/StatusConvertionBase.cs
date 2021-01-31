using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Extentions;
using UnoTest.Shared.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UnoTest.Shared.Converters
{
    public abstract class StatusConvertionBase
    {
        protected SolidColorBrush trueColor = (SolidColorBrush)Application.Current.Resources["CorrectAnswerBrush"];
        protected SolidColorBrush noColor = (SolidColorBrush)Application.Current.Resources["NoAnswerBrush"];
        protected SolidColorBrush falseColor = (SolidColorBrush)Application.Current.Resources["FalseAnswerBrush"];

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
