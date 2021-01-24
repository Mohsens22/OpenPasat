using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestHostViewModel : ViewModelBase, IScreen
    {
        public TestHostViewModel()
        {
            Router = new RoutingState();
            Router.Navigate.Execute(new StartUpViewModel(this));
        }
        public RoutingState Router { get; set; }
    }
}
