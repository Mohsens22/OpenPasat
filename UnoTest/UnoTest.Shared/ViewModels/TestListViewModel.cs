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
using UnoTest.Extentions;
using UnoTest.Models;
using UnoTest.Shared.Logic;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestListViewModel : RoutableViewModel
    {
        public TestListViewModel(IScreen screen,User user):base(screen)
        {
            ActiveUser = user;
            this.WhenActivated(d =>
            {
                Tests = TestManager.GetTestsFor(ActiveUser);

                this.WhenAnyValue(x => x.SelectedTest)
                .WhereNotNull().Subscribe(async s =>
                {
                    var sheet = await TestManager.EagerLoad(SelectedTest);
                    HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, sheet));
                    SelectedTest = null;
                }
                )
                .DisposeWith(d);
            });
        }
        [Reactive]
        public List<TestIndentifier> Tests { get; set; }

        [Reactive]
        public TestIndentifier SelectedTest { get; set; }
        public User ActiveUser { get; set; }
        public override string ToString() => "TestsVM";
    }
}
