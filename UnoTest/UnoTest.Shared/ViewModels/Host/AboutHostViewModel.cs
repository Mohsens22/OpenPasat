using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutHostViewModel : ViewModelBase, IScreen
    {
        public AboutHostViewModel()
        {
            Router = new RoutingState();
            Router.Navigate.Execute(new AboutViewModel());
        }
        public RoutingState Router { get; set; }
    }
}
