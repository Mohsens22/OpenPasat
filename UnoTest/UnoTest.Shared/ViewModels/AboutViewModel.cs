using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AboutViewModel:ViewModelBase, IActivatableViewModel, IRoutableViewModel
    {
        public string UrlPathSegment => this.ToString();

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public IScreen HostScreen { get; set; }
    }
}
