using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Text;
using UnoTest.Shared.Views;
using Windows.UI.Xaml.Input;

namespace UnoTest.Shared.ViewModels
{
    public class StartPageViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public StartPageViewModel()
        {
            Locator.CurrentMutable.Register(() => new StartUpView(), typeof(IViewFor<StartUpViewModel>));
            Router.Navigate.Execute(new StartUpViewModel(this));
        }


        public override string ToString() => "StartPageVM";

    }
}
