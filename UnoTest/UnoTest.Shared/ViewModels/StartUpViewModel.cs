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
using UnoTest.Shared.Models;
using UnoTest.Shared.UserModels;
using Windows.Storage;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class StartUpViewModel : ViewModelBase, IActivatableViewModel,IRoutableViewModel
    {
        public StartUpViewModel(IScreen screen)
        {
            HostScreen = screen;
            Identifier = new TestIndentifier { ImpulseRate = 200, Quantum = 3000, TestCount = 60,Correction=false };
            NavigateCommand = ReactiveCommand.Create(StartTest);
            TestCommand = ReactiveCommand.Create(DoTest);
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
        }

        private async void DoTest()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SampleSheetStandard.Json"));
            var txt = await FileIO.ReadTextAsync(file);
            var sheet = JsonConvert.DeserializeObject<TestSheet>(txt);
            await HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, sheet));
        }

        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        public ReactiveCommand<Unit, Unit> TestCommand { get; set; }
        private void StartTest()=> HostScreen.Router.Navigate.Execute(new TestViewModel(HostScreen,Identifier));

        [Reactive]
        public TestIndentifier Identifier { get; set; }
        [Reactive]
        public RepresentationTypeLookup SelectedRepresentation { get; set; }
        [Reactive]
        public List<RepresentationTypeLookup> Representations { get; set; }

        public string UrlPathSegment => this.ToString();

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public IScreen HostScreen { get; set; }

        public override string ToString() => "StartUpVM";
    }
}
