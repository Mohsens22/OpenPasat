using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnoTest.Shared.Logic;

namespace UnoTest.ViewModels
{
    public class UserHostViewModel:HostViewModel
    {
        public UserHostViewModel() : base()
        {
            Router.Navigate.Execute(new UsersViewModel(this));
        }
        public UserHostViewModel(string username) : base()
        {
            var user = UserManager.GetUsers(username).FirstOrDefault();
            Router.Navigate.Execute(new TestListViewModel(this,user));
        }
        public override string ToString() => "TestHostVM";
    }
}
