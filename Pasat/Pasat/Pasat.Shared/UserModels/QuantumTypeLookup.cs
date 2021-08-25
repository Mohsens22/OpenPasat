using Pasat.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pasat.UserModels
{
    public class QuantumTypeLookup
    {
        public Quantum Item { get; set; }
        public string Display { get; set; }
        public static List<QuantumTypeLookup> Load()
        {
            var list = new List<QuantumTypeLookup>();
#if DEBUG
            list.Add(new QuantumTypeLookup
            {
                Display= "250" + LanguageHelper.GetString("Milliseconds", "Text"),
                Item=Quantum.DevMinimum
            });
#endif
            list.Add(new QuantumTypeLookup
            {
                Display = "1" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.OneSec
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "1.3" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.OneThree
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "1.5" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.OneHalf
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "2" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.Two
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "2.5" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.TwoHalf
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "3" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.Three
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "3.5" + LanguageHelper.GetString("Seconds", "Text"),
                Item = Quantum.ThreeHalf
            });
            list.Add(new QuantumTypeLookup
            {
                Display = "4"+LanguageHelper.GetString("Seconds","Text"),
                Item = Quantum.Four
            });
            return list;
        }
    }
    public enum Quantum
    {
        DevMinimum=250,
        OneSec=1000,
        OneThree=1300,
        OneHalf=1500,
        Two=2000,
        TwoHalf=2500,
        Three=3000,
        ThreeHalf=3500,
        Four=4000
    }
}
