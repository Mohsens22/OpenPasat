using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using UnoTest.Shared.Logic;
using UnoTest.Shared.Models;
using UnoTest.Shared.UserModels;
using Windows.Storage;
using Olive;
using UnoTest.Shared.Extentions;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class StartUpViewModel : RoutableViewModel
    {
        public StartUpViewModel(IScreen screen):base(screen)
        {
            Identifier = new TestIndentifier { ImpulseRate = 200, Quantum = 2500, TestCount = 10,Correction=true };
            NavigateCommand = ReactiveCommand.Create(StartTest);

            Representations = RepresentationTypeLookup.Load();

            SelectedRepresentation = Representations.FirstOrDefault();
            this.WhenActivated(disposables =>
            {
                this
                .WhenAnyValue(x => x.SelectedRepresentation)
                .WhereNotNull()
                .Subscribe(x => Identifier.RepresentationType = x.Item)
                .DisposeWith(disposables) ;
            });

#if DEBUG
            TestCommand = ReactiveCommand.Create(DoTest);
            LoadSheet = ReactiveCommand.Create(Loadshite);
#endif
        }

#if DEBUG
        private async void DoTest()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SampleSheetStandard.Json"));
            var txt = await FileIO.ReadTextAsync(file);
            var sheet = JsonConvert.DeserializeObject<TestSheet>(txt);
            await HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, sheet));
        }
        private async void Loadshite()
        {
            var sheet = TestFactory.Load(Identifier);
            var lines = sheet.TestFragments.Select(x => x.ToString()).ToList();
            lines.Add(sheet.TestInfo.ToString());
            lines.ToLinesString().CopyToClipboard();
        }
#endif

        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        public ReactiveCommand<Unit, Unit> TestCommand { get; set; }
        public ReactiveCommand<Unit, Unit> LoadSheet { get; set; }
        private void StartTest()=> HostScreen.Router.Navigate.Execute(new TestViewModel(HostScreen,Identifier));

        [Reactive]
        public TestIndentifier Identifier { get; set; }
        [Reactive]
        public RepresentationTypeLookup SelectedRepresentation { get; set; }
        [Reactive]
        public List<RepresentationTypeLookup> Representations { get; set; }

        public override string ToString() => "StartUpVM";
    }
}
