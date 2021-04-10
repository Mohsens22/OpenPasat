using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnoTest.Services
{
    class FileSaver : ISaver
    {
        public async Task Save(string suggestedName, byte[] bytes, string type, params string[] types)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = $"{type}(*{types.FirstOrDefault()})|*{types.FirstOrDefault()}",
                AddExtension=true,
                FileName=suggestedName,
                DefaultExt=types.FirstOrDefault(),
                Title="Save as"
            };

            if (dialog.ShowDialog() ==DialogResult.OK)
            {
                await File.WriteAllBytesAsync(dialog.FileName, bytes);
            }
        }
    }
}
