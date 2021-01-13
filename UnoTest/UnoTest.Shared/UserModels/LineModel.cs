using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.UserModels
{
    public class LineModel
    {
        public LineModel()
        {

        }
        public LineModel(double yVal1, double yVal2, double size, string str)
        {
            YValue = yVal1;
            Value = yVal2;
            Size = size;
            XValue = str;
        }
        public string XValue { get; set; }

        public double YValue { get; set; }

        public double Value { get; set; }

        public double Size { get; set; }
    }

    public class Model
    {
        public string Country { get; set; }

        public string Name { get; set; }
        public double Count { get; set; }

        public string Year { get; set; }
    }
}
