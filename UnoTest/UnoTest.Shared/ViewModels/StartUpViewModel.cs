using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.ViewModels
{
    public class StartUpViewModel : ReactiveObject, IRoutableViewModel
    {
        public StartUpViewModel(IScreen screen)
        {
            HostScreen = screen;
            Identifier = new TestIndentifier { ImpulseRate = 200, Quantum = 3000, TestCount = 60 };
        }

        [Reactive]
        public TestIndentifier Identifier { get; set; }

        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        public override string ToString() => "StartUpVM";
    }
}
