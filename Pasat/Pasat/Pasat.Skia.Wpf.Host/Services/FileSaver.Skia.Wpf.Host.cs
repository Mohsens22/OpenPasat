using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Pasat.Services
{
    class FileSaver : ISaver
    {
        public async Task Save(string suggestedName, byte[] bytes, string type, params string[] types)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = $"{type}(*{types.FirstOrDefault()})|*{types.FirstOrDefault()}",
                AddExtension = true,
                FileName = suggestedName,
                DefaultExt = types.FirstOrDefault(),
                Title = "Save as"
            };
            var r = dialog.ShowDialog();
            if (r.HasValue && r.Value)
            {
                await File.WriteAllBytesAsync(dialog.FileName, bytes);
            }
        }
    }
}
