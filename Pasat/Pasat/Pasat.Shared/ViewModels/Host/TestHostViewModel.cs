using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pasat.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestHostViewModel : HostViewModel
    {
        public TestHostViewModel():base()
        {
            Router.Navigate.Execute(new StartUpViewModel(this));
        }
        public override string ToString() => "TestHostVM";
    }
}
