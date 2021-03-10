using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using UnoTest.Models;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TutorialViewModel : RoutableViewModel
    {
        public TutorialViewModel(IScreen screen) : base(screen)
        {
            NavigateCommand = ReactiveCommand.Create(StartTest);
        }
        private void StartTest()
        {
            var Identifier = new TestIndentifier { ImpulseRate = 200, Correction = true,Quantum=3000,TestCount=10,RepresentationType=RepresentationType.UI };
            HostScreen.Router.Navigate.Execute(new ValidationViewModel(HostScreen, Identifier, null));
        }
        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        public override string ToString() => "Tutorial VM";
    }
}
