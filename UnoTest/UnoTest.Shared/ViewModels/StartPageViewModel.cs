using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Text;
using Windows.UI.Xaml.Input;

namespace UnoTest.Shared.ViewModels
{
    public class StartPageViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        public StartPageViewModel()
        {
            Title = "Hello Joel";
            InputCommand = ReactiveCommand.Create(ChangeName);
            //Locator.CurrentMutable.Register(() => new FirstView(), typeof(IViewFor<FirstViewModel>));
        }

        private void ChangeName(KeyRoutedEventArgs obj)
        {
            throw new NotImplementedException();
        }

        public ReactiveCommand<KeyRoutedEventArgs, Unit> InputCommand { get; }

        [Reactive]
        public string Title { get; set; }


        public override string ToString() => "StartPageVM";

    }
}
