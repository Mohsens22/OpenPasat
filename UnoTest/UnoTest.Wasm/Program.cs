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
            MonoInternals.mono_trace_enable(1);
            MonoInternals.mono_trace_set_options("E:all");

#pragma warning disable CS0618 // Type or member is obsolete
            PlatformEnlightenmentProvider.Current.EnableWasm();
#pragma warning restore CS0618 // Type or member is obsolete

#if DEBUG
            FeatureConfiguration.UIElement.AssignDOMXamlName = true;
#endif
            Application.Start(_ => _app = new App());

            return 0;
        }
    }
}
