using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace UnoTest.Models
{
    [Windows.UI.Xaml.Data.Bindable]
    public class User:BaseModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Job { get; set; }
        public Education Education { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Gender Gender { get; set; }
        public string ClinicalHistory { get; set; }
        public string DrugAbuseHistory { get; set; }
        public string OtherInfo { get; set; }
        public DateTimeOffset? YearBorn { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int? Age 
        {
            get 
            {
                if (YearBorn.HasValue)
                {
                    return DateTimeOffset.Now.Year - YearBorn.Value.Year;
                }
                else
                    return null;
                
            }
            set 
            {
                YearBorn = DateTimeOffset.UtcNow.AddYears(-value.Value);
            }
        }
        [JsonIgnore]
        public int TestCount { get; set; }
        [JsonIgnore]
        public List<TestIndentifier> Tests { get; set; }
        public override string ToString() => $"{FullName} (@{Username})";
    }
}
