using System;
using System.Reactive.PlatformServices;
using Uno.UI;
using Windows.UI.Xaml;

namespace UnoTest.Wasm
{
    public class Program
    {
        private static App _app;

        static int Main(string[] args)
        {

            SQLitePCL.Batteries.Init();
            Application.Start(_ => _app = new App());

            return 0;
        }
    }
}
