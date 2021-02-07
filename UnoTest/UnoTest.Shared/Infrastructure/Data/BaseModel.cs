using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public override string ToString() => Id.ToString();
    }
}
