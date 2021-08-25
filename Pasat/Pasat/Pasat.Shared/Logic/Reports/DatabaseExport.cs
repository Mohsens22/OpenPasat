using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Pasat.Infrastructure;
using Pasat.Services;

namespace Pasat.Logic.Reports
{
    static class DatabaseExport
    {
        public static async Task SaveSqlite()
        {
            var picker = Locator.Current.GetService<ISaver>();
            var bytes = File.ReadAllBytes(Constants.SQLiteFilePath);
            await picker.Save("data.sqlite", bytes, "SQLite", ".sqlite");
        }
    }
}
