using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Extentions;
using Olive;
using UnoTest.Shared.Infrastructure.Features;
using UnoTest.Shared.Infrastructure;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutViewModel: RoutableViewModel
    {
        public AboutViewModel(IScreen screen) : base(screen)
        {
            Features = App.Features.GetFeatures().Where(x=>x.Value==FeatureAvailability.Available).Select(x=>x.Key).ToLinesString();
            VersionInfo = $"v{Constants.AppVersion} Preview";
#if DEBUG
            VersionInfo += " - (dev)";
#endif

#if __WASM__
            VersionInfo +=  " ,Mode: "+Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_MODE");
#endif
        }


        public string Features { get; set; }
        public string VersionInfo { get; set; }
        public override string ToString() => "About VM";
    }
}
