﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class UsersViewModel : RoutableViewModel
    {
        public UsersViewModel(IScreen screen) : base(screen)
        {

        }
        public override string ToString() => "UsersVM";
    }
}
