using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using UnoTest.Shared.Models;

namespace UnoTest.Shared.ViewModels
{
    public class TestViewModel: ReactiveObject, IRoutableViewModel
    {
        public TestViewModel(IScreen screen, TestIndentifier indentifier=null)
        {
            HostScreen = screen;
        }
        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        public bool IsResultTaken { get; set; }
        public bool CanInput { get; set; }


        public override string ToString() => "TestVM";
    }
}
