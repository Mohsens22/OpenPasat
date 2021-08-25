using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Windows.System;
using Olive;
using Newtonsoft.Json;

namespace Pasat.Models
{
    public class ValidationContext
    {
        public ValidationContext()
        {
            Items = new List<ValidationItem>();
        }


        public bool IsTestValid { get; set; }
        public long? OverallReactionTime { get; set; }
        public List<ValidationItem> Items { get; set; }

        public void Validate()
        {
            IsTestValid = Items.All(x=>x.Correction==CorrectionStatus.True);

            if (IsTestValid)
            {
                OverallReactionTime = Items.Sum(x => x.Speed)/Items.Count;
            }
        }

        public static string ToJson(ValidationContext context) => JsonConvert.SerializeObject(context, Formatting.Indented);
        public static ValidationContext FromJson(string context) => JsonConvert.DeserializeObject<ValidationContext>(context);

        public override string ToString() => $"{IsTestValid} {OverallReactionTime}";
    }
    public class ValidationItem
    {
        public VirtualKey Key { get; set; }
        public CorrectionStatus Correction { get; set; }
        public long? Speed { get; set; }
        public InputType InputType { get; set; }
        public DateTimeOffset RepresentedAt { get; set; }
        public DateTimeOffset AnsweredAt { get; set; }
        
        

        public override string ToString() => $"{Key}- {Correction} {Speed}";
    }
}
