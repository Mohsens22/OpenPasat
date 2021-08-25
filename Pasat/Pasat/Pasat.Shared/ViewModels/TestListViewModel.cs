using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Uwp;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Pasat.Extentions;
using Pasat.Models;
using Pasat.Logic;
using System.Collections.ObjectModel;
using Uno.Extensions;

namespace Pasat.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestListViewModel : RoutableViewModel
    {
        public TestListViewModel(IScreen screen,User user):base(screen)
        {
            ActiveUser = user;
            Tests = new ObservableCollection<TestIndentifier>();
            LoadPage();
            this.WhenActivated(disposables => 
            {
                this.WhenAnyValue(x => x.SelectedTest)
               .WhereNotNull().Subscribe(async s =>
               {
                   var sheet = await TestManager.EagerLoad(SelectedTest);
                   HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, sheet));
                   SelectedTest = null;
               }
               );
                Disposable
               .Create(() =>
               {
                   ReactiveFactory.UserChanged -= ReactiveFactory_UserChanged;
               }).DisposeWith(disposables);
            });

            ReactiveFactory.UserChanged += ReactiveFactory_UserChanged;
        }

        private void ReactiveFactory_UserChanged(object sender, User e)
        {
            if (e.Id == ActiveUser.Id)
            {
                Tests.Clear();
                var all = TestManager.GetTestsFor(ActiveUser);
                Tests.AddRange(all);
            }
        }

        private void LoadPage()
        {
            Tests.AddRange(TestManager.GetTestsFor(ActiveUser));
        }

        [Reactive]
        public ObservableCollection<TestIndentifier> Tests { get; set; }

        [Reactive]
        public TestIndentifier SelectedTest { get; set; }
        [Reactive]
        public User ActiveUser { get; set; }
        public override string ToString() => "TestsVM";
    }
}
