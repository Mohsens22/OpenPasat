using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using UnoTest.Logic;
using UnoTest.Models;
using UnoTest.UserModels;
using Windows.Storage;
using Olive;
using UnoTest.Extentions;
using System.Text.Json;
using System.Collections.ObjectModel;
using UnoTest.Shared.Logic;
using System.Diagnostics;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class StartUpViewModel : RoutableViewModel
    {
        const int _minimumImpulseRate = 200;
        public StartUpViewModel(IScreen screen):base(screen)
        {
            Identifier = new TestIndentifier { ImpulseRate = _minimumImpulseRate, Correction =false };
            Quantum = 2500;
            TestCount = 10;

            NavigateCommand = ReactiveCommand.Create(StartTest);

            Representations = RepresentationTypeLookup.Load();

            SelectedRepresentation = Representations.FirstOrDefault();
            SelectedUser =UserManager.GetDefaultUser();
            SuggestedUsers = new ObservableCollection<User>();
            this.WhenActivated(disposables =>
            {
                this
                .WhenAnyValue(x => x.Quantum)
                .WhereNotNull()
                .Subscribe(x =>
                {
                    var suggested = x / 10;
                    if (suggested>_minimumImpulseRate)
                    {
                        Identifier.ImpulseRate = suggested;
                    }
                    else
                    {
                        Identifier.ImpulseRate = _minimumImpulseRate;
                    }
                });
                this
                .WhenAnyValue(x => x.SelectedRepresentation)
                .WhereNotNull()
                .Subscribe(x => Identifier.RepresentationType = x.Item)
                .DisposeWith(disposables) ;

                this.WhenAnyValue(x => x.SearchTerm)
                .WhereNotNull()
                .Subscribe(term => Search(term));
            });


        }
        [Reactive]
        public int TestCount { get; set; }
        [Reactive]
        public int Quantum { get; set; }


        public void Search(string item)
        {
            if (item.IsEmpty())
            {
                return;
            }
            if (item.Length < 3)
            {
                return;
            }
            SuggestedUsers.Clear();
            SuggestedUsers.AddRange(UserManager.GetUsers(item));
        }

        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        private void StartTest()
        {
            Identifier.UserId = SelectedUser.Id;
            Identifier.TestCount = TestCount;
            Identifier.Quantum = Quantum;
            HostScreen.Router.Navigate.Execute(new TestViewModel(HostScreen, Identifier, SelectedUser));
        }

        [Reactive]
        public TestIndentifier Identifier { get; set; }
        [Reactive]
        public RepresentationTypeLookup SelectedRepresentation { get; set; }
        [Reactive]
        public List<RepresentationTypeLookup> Representations { get; set; }

        [Reactive]
        public ObservableCollection<User> SuggestedUsers { get; set; }

        [Reactive]
        public User SelectedUser { get; set; }
        [Reactive]
        public string SearchTerm { get; set; }

        public override string ToString() => "StartUpVM";
    }
}
