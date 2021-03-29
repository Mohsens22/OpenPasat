using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnoTest.Infrastructure
{
    public static class Constants
    {
        //Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "pasat.db")
        public static string SQLiteFilePath { get => Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "pasat.sqlite"); }
        public static string SQLiteFileName { get => $"data source={Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "pasat.sqlite")}"; }
        public static string AppVersion { get => "1.1"; }
    }
}
