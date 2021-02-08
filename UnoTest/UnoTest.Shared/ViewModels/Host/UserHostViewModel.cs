using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.ViewModels
{
    public class UserHostViewModel:HostViewModel
    {
        public UserHostViewModel() : base()
        {
            Router.Navigate.Execute(new UsersViewModel(this));
        }
        public override string ToString() => "TestHostVM";
    }
}
