using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using Pasat.Models;
using Pasat.UserModels;
using Olive;
using System.Linq;
using System.Reactive;
using Pasat.Extentions;
using Pasat.Logic;
using ReactiveUI.Validation.Extensions;
using System.Text.RegularExpressions;
using System.Reactive.Linq;

namespace Pasat.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class AddUserViewModel : RoutableViewModel
    {
        public AddUserViewModel(IScreen screen):base(screen)
        {
            User = new User();
            Age = 0;
            Username = "";
            FullName = "";
            Educations = EducationTypeLookup.Load();
            Genders = GenderTypeLookup.Load();
            MaritalStatus = MaritalStatusTypeLookup.Load();
            SelectedEducation = Educations.FirstOrDefault();
            SelectedGender = Genders.FirstOrDefault();
            SelectedMarital = MaritalStatus.FirstOrDefault();
            Insert = ReactiveCommand.Create(AddUser);

            this.WhenActivated(d =>
            {
                this.WhenAnyValue(x => x.FullName)
                .WhereNotNull()
                .Where(x=>x.HasValue())
                .Where(x=>this.Username.IsEmpty())
                .Subscribe(x =>
                {
                    if (!x.HasSpecialCharacters())
                    {
                        var i = 0;
                        var suggestedUsername = x.Remove(" ", "\n");
                        while (UserManager.UserExists(suggestedUsername))
                        {
                            i += 1;
                            suggestedUsername = suggestedUsername + i;
                        }
                        Username = suggestedUsername;
                    }
                }).DisposeWith(d);
            });

            this.ValidationRule(vm => vm.FullName,
                name => name.HasValue() && name.Length>2,
                LanguageHelper.GetString("MustProvideAge", "Text"));

            this.ValidationRule(vm => vm.Username,
                name => name.HasValue()&&name.Length>2,
                LanguageHelper.GetString("MustProvideUserName", "Text"));

            this.ValidationRule(vm => vm.Username,
                name => !name.HasSpecialCharacters()&&!name.ContainsAny(' ','\n'),
                LanguageHelper.GetString("UserNameHasInvalidChars","Text"));

            this.ValidationRule(vm => vm.Username,
                name => !UserManager.UserExists(name),
                LanguageHelper.GetString("UserExists", "Text"));

            this.ValidationRule(vm => vm.Age,
                age => age>4,
                LanguageHelper.GetString("MustProvideAge", "Text"));

        }

        [Reactive]
        public string FullName { get; set; }
        [Reactive]
        public string Username { get; set; }


        [Reactive]
        public User User { get; set; }
        [Reactive]
        public int Age { get; set; }

        public List<GenderTypeLookup> Genders { get; set; }
        public List<MaritalStatusTypeLookup> MaritalStatus { get; set; }
        public List<EducationTypeLookup> Educations { get; set; }
        [Reactive]
        public GenderTypeLookup SelectedGender { get; set; }
        [Reactive]
        public MaritalStatusTypeLookup SelectedMarital { get; set; }
        [Reactive]
        public EducationTypeLookup SelectedEducation { get; set; }


        public ReactiveCommand<Unit,Unit> Insert { get; set; }
        void AddUser()
        {
            User.FullName = FullName;
            User.Username = Username.ToLower();
            User.Gender = SelectedGender.Item;
            User.MaritalStatus = SelectedMarital.Item;
            User.Education = SelectedEducation.Item;
            if (Age>2)
            {
                User.Age = Age;
            }
            UserManager.AddUser(User);
            HostScreen.Router.NavigateBack.Execute();
        }

        public override string ToString() => "AddUserVM";
    }
}
