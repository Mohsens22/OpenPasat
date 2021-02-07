using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
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
        public DateTimeOffset YearBorn { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public override string ToString() => $"{Id}-{FullName}";
    }
}
