using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.ViewModels
{
    public class AddUserViewModel : RoutableViewModel
    {
        public AddUserViewModel(IScreen screen):base(screen)
        {

        }
        public override string ToString() => "AddUserVM";
    }
}
