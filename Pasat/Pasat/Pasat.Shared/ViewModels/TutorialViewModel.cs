using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Pasat.Models;
using Windows.Media.Core;
using Pasat.Extentions;

namespace Pasat.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TutorialViewModel : RoutableViewModel
    {
        public TutorialViewModel(IScreen screen) : base(screen)
        {
            NavigateCommand = ReactiveCommand.Create(StartTest);

            Audio1 = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/{LanguageHelper.GetTag()}/Audio/101.mp3"));
            Audio2 = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/{LanguageHelper.GetTag()}/Audio/102.mp3"));
            Audio3 = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/{LanguageHelper.GetTag()}/Audio/103.mp3"));
        }
        private void StartTest()
        {
            var Identifier = new TestIndentifier { ImpulseRate = 200, Correction = true,Quantum=3000,TestCount=10,RepresentationType=RepresentationType.UI };
            HostScreen.Router.Navigate.Execute(new ValidationViewModel(HostScreen, Identifier, null));
        }

        public MediaSource Audio1 { get; set; }
        public MediaSource Audio2 { get; set; }
        public MediaSource Audio3 { get; set; }

        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        public override string ToString() => "Tutorial VM";
    }
}
