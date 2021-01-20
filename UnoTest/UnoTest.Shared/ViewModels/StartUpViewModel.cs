using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using UnoTest.Shared.Models;
using Windows.Storage;

namespace UnoTest.Shared.ViewModels
{
    public class StartUpViewModel : ViewModelBase, IActivatableViewModel,IRoutableViewModel
    {
        public StartUpViewModel(IScreen screen)
        {
            HostScreen = screen;
            Identifier = new TestIndentifier { ImpulseRate = 200, Quantum = 3000, TestCount = 60,Correction=false };
            NavigateCommand = ReactiveCommand.Create(StartTest);
            TestCommand = ReactiveCommand.Create(DoTest);
        }

        private async void DoTest()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SampleSheet.Json"));
            var txt = await FileIO.ReadTextAsync(file);
            var sheet = JsonConvert.DeserializeObject<TestSheet>(txt);
            HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, sheet));
        }

        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        public ReactiveCommand<Unit, Unit> TestCommand { get; set; }
        private void StartTest()=> HostScreen.Router.Navigate.Execute(new TestViewModel(HostScreen,Identifier));

        [Reactive]
        public TestIndentifier Identifier { get; set; }



        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public override string ToString() => "StartUpVM";
    }
}
