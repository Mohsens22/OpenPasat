using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutHostViewModel : HostViewModel
    {
        public AboutHostViewModel():base()
        {
            Router.Navigate.Execute(new AboutViewModel(this));
        }

        public override string ToString() => "AboutHost";
    }
}
