using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pasat.Services
{
    public interface ISaver
    {
        Task Save(string suggestedName, byte[] bytes, string type, params string[] types);
    }
}
