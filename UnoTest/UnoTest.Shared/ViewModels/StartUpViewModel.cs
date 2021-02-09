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
        public StartUpViewModel(IScreen screen):base(screen)
        {
            Identifier = new TestIndentifier { ImpulseRate = 200, Quantum = 2500, TestCount = 10,Correction=false };
            NavigateCommand = ReactiveCommand.Create(StartTest);

            Representations = RepresentationTypeLookup.Load();

            SelectedRepresentation = Representations.FirstOrDefault();
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
                .Subscribe(term => Search(term));
            });

#if DEBUG
            TestCommand = ReactiveCommand.Create(DoTest);
            LoadSheet = ReactiveCommand.Create(Loadshite);
#endif
        }

#if DEBUG
        private async void DoTest()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SampleSheetStandard.Json"));
            var txt = await FileIO.ReadTextAsync(file);
            var sheet = JsonSerializer.Deserialize<TestIndentifier>(txt);
            await HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, sheet));
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
        private async void Loadshite()
        {
            TestFactory.Load(Identifier);
            var lines = Identifier.TestFragments.Select(x => x.ToString()).ToList();
            lines.Add(Identifier.ToString());
            lines.ToLinesString().CopyToClipboard();
        }
#endif

        public ReactiveCommand<Unit, Unit> NavigateCommand { get; set; }
        public ReactiveCommand<Unit, Unit> TestCommand { get; set; }
        public ReactiveCommand<Unit, Unit> LoadSheet { get; set; }
        private void StartTest()=> HostScreen.Router.Navigate.Execute(new TestViewModel(HostScreen,Identifier));

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
