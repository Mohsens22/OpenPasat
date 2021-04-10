using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Extentions;
using Olive;
using UnoTest.Infrastructure.Features;
using UnoTest.Infrastructure;
using System.Reactive;
using UnoTest.Logic.Reports;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutViewModel: RoutableViewModel
    {
        public AboutViewModel(IScreen screen) : base(screen)
        {
            Features = App.Features.GetFeatures().Where(x=>x.Value==FeatureAvailability.Available).Select(x=>x.Key).ToLinesString();
            SaveSql = ReactiveCommand.CreateFromTask(DatabaseExport.SaveSqlite);
            VersionInfo = $"v{Constants.AppVersion} Preview";
#if DEBUG
            VersionInfo += " - (dev)";
#endif

#if __WASM__
            VersionInfo +=  " ,Mode: "+Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_MODE");
#endif
        }

        public ReactiveCommand<Unit,Unit> SaveSql { get; set; }
        public string Features { get; set; }
        public string VersionInfo { get; set; }
        public override string ToString() => "About VM";
    }
}
