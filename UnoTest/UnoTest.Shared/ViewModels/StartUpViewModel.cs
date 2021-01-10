using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.ViewModels
{
    public class StartUpViewModel : ReactiveObject, IRoutableViewModel
    {
        public StartUpViewModel(IScreen screen) => HostScreen = screen;

        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        public override string ToString() => "StartUpVM";
    }
}
