using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Extentions;
using Olive;
using UnoTest.Shared.Infrastructure.Features;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutViewModel: RoutableViewModel
    {
        public AboutViewModel(IScreen screen) : base(screen)
        {
            Features = App.Features.GetFeatures().Where(x=>x.Value==FeatureAvailability.Available).Select(x=>x.Key).ToLinesString();
#if __WASM__
            Features += Environment.NewLine + Environment.NewLine + "Mode: "+Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_MODE");
#endif
        }

        public string Features { get; set; }
        public override string ToString() => "About VM";
    }
}
