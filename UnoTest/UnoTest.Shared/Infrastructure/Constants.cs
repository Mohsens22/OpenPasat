using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnoTest.Shared.Infrastructure
{
    public static class Constants
    {
        public static string SQLiteFileName { get => Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "pasat.db"); }
        public static string AppVersion { get => "1.0"; }
    }
}
