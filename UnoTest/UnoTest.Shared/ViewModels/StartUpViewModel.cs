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
using ReactiveUI.Validation.Extensions;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class StartUpViewModel : RoutableViewModel
    {
        const int _minimumImpulseRate = 200;
        public StartUpViewModel(IScreen screen):base(screen)
        {
            IsDbAvailable = App.Features.InAppDatabase == Infrastructure.Features.FeatureAvailability.Available;
            Identifier = new TestIndentifier { ImpulseRate = _minimumImpulseRate, Correction =false };

            NavigateCommand = ReactiveCommand.Create(StartTest);
            NavigateToturialMode = ReactiveCommand.Create(StartTurotial);
            Quantums = QuantumTypeLookup.Load();
            Counts = TestCountTypeLookup.Load();
            SelectedQuantum = Quantums.FirstOrDefault(x => x.Item == Quantum.TwoHalf);
            SelectedCount = Counts.FirstOrDefault(x => x.Item == TestCount.Sixty);
            Representations = RepresentationTypeLookup.Load();

            SelectedRepresentation = Representations.FirstOrDefault(x=>x.Item==RepresentationType.UI);
            SelectedUser =UserManager.GetDefaultUser();
            SuggestedUsers = new ObservableCollection<User>();
            this.WhenActivated(disposables =>
            {
                this
                .WhenAnyValue(x => x.SelectedRepresentation)
                .WhereNotNull()
                .Subscribe(x => Identifier.RepresentationType = x.Item)
                .DisposeWith(disposables) ;

                this.WhenAnyValue(x => x.SearchTerm)
                .WhereNotNull()
                .Subscribe(term => Search(term.Trim()));
            });
           

        }


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
        public ReactiveCommand<Unit, Unit> NavigateToturialMode { get; set; }
        private void StartTurotial() => HostScreen.Router.Navigate.Execute(new TutorialViewModel(HostScreen));
        private void StartTest()
        {
            Identifier.UserId = SelectedUser.Id;
            Identifier.TestCount = (int)SelectedCount.Item;
            Identifier.Quantum = (int)SelectedQuantum.Item;
            HostScreen.Router.Navigate.Execute(new ValidationViewModel(HostScreen, Identifier, SelectedUser,AllowInvalid));
        }

        public bool IsDbAvailable { get; set; }

        [Reactive]
        public bool AllowInvalid { get; set; }

        [Reactive]
        public TestIndentifier Identifier { get; set; }
        [Reactive]
        public RepresentationTypeLookup SelectedRepresentation { get; set; }
        [Reactive]
        public List<RepresentationTypeLookup> Representations { get; set; }

        [Reactive]
        public TestCountTypeLookup SelectedCount { get; set; }
        [Reactive]
        public List<TestCountTypeLookup> Counts { get; set; }

        [Reactive]
        public QuantumTypeLookup SelectedQuantum { get; set; }
        [Reactive]
        public List<QuantumTypeLookup> Quantums { get; set; }

        [Reactive]
        public ObservableCollection<User> SuggestedUsers { get; set; }

        [Reactive]
        public User SelectedUser { get; set; }
        [Reactive]
        public string SearchTerm { get; set; }

        public override string ToString() => "StartUpVM";
    }
}
