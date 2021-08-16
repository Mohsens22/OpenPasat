using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pasat.Extentions;
using Olive;
using Pasat.Infrastructure.Features;
using Pasat.Infrastructure;
using System.Reactive;
using Pasat.Logic.Reports;
using ReactiveUI.Fody.Helpers;
using Pasat.UserModels;
using System.Reactive.Disposables;

namespace Pasat.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutViewModel: RoutableViewModel
    {
        public AboutViewModel(IScreen screen) : base(screen)
        {
            Features = App.Features.GetFeatures().Where(x=>x.Value==FeatureAvailability.Available).Select(x=>x.Key).ToLinesString();
            SaveSql = ReactiveCommand.CreateFromTask(DatabaseExport.SaveSqlite);
            VersionInfo = $"v{Constants.AppVersion} Preview";
            Languages = LanguageLookup.Load();
            SelectedLanguage = Languages.FirstOrDefault(x => x.Item == LanguageHelper.AppLanguage);

            this.WhenActivated(disposables =>
            {
                this
                .WhenAnyValue(x => x.SelectedLanguage)
                .WhereNotNull()
                .Subscribe(x => LanguageHelper.AppLanguage = x.Item)
                .DisposeWith(disposables);

            });
#if DEBUG
            VersionInfo += " - (dev)";
#endif

#if __WASM__
            VersionInfo +=  " ,Mode: "+Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_MODE");
#endif
        }

        [Reactive]
        public LanguageLookup SelectedLanguage { get; set; }
        [Reactive]
        public List<LanguageLookup> Languages { get; set; }

        public ReactiveCommand<Unit,Unit> SaveSql { get; set; }
        public string Features { get; set; }
        public string VersionInfo { get; set; }
        public override string ToString() => "About VM";
    }
}
