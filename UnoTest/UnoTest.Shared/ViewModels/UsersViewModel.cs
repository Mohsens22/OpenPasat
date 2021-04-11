using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Uwp;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnoTest.Models;
using UnoTest.Logic;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class UsersViewModel : RoutableViewModel
    {
        public UsersViewModel(IScreen screen) : base(screen)
        {
            
            Add = ReactiveCommand.Create(AddAction);

            this.WhenActivated(d =>
            {
                Users = new IncrementalLoadingCollection<UserSource, User>();

                this.WhenAnyValue(x => x.SelectedUser)
                .WhereNotNull().Subscribe(s => 
                {
                    HostScreen.Router.Navigate.Execute(new TestListViewModel(HostScreen,SelectedUser));
                    SelectedUser = null;
                })
                .DisposeWith(d);
            }) ;

        }
        [Reactive]
        public IncrementalLoadingCollection<UserSource, User> Users { get; set; }

        [Reactive]
        public User SelectedUser { get; set; }
        [Reactive]
        public ReactiveCommand<Unit, Unit> Add { get; set; }
        public override string ToString() => "UsersVM";
        void AddAction()
        {
            HostScreen.Router.Navigate.Execute(new AddUserViewModel(HostScreen));
        }

    }

    public class UserSource : IIncrementalSource<User>
    {
        public async Task<IEnumerable<User>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var users = UserManager.GetUsers(pageIndex, pageSize); 
            return users;
            
        }
    }
}
