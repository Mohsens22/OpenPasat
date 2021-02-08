using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Uwp;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnoTest.Models;
using UnoTest.Shared.Logic;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class UsersViewModel : RoutableViewModel
    {
        public UsersViewModel(IScreen screen) : base(screen)
        {
            Users= new IncrementalLoadingCollection<UserSource, User>();


        }
        [Reactive]
        public IncrementalLoadingCollection<UserSource, User> Users { get; set; }
        public override string ToString() => "UsersVM";
    }

    public class UserSource : IIncrementalSource<User>
    {
        public async Task<IEnumerable<User>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return UserManager.GetUsers(pageIndex, pageSize);
            
        }
    }
}
