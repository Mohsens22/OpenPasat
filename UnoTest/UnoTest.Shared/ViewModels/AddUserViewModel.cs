using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using UnoTest.Models;
using UnoTest.UserModels;
using Olive;
using System.Linq;
using System.Reactive;
using UnoTest.Extentions;
using UnoTest.Shared.Logic;

namespace UnoTest.ViewModels
{
    public class AddUserViewModel : RoutableViewModel
    {
        public AddUserViewModel(IScreen screen):base(screen)
        {
            User = new User();
            Educations = EducationTypeLookup.Load();
            Genders = GenderTypeLookup.Load();
            MaritalStatus = MaritalStatusTypeLookup.Load();
            SelectedEducation = Educations.FirstOrDefault();
            SelectedGender = Genders.FirstOrDefault();
            SelectedMarital = MaritalStatus.FirstOrDefault();
            Insert = ReactiveCommand.Create(AddUser);
            
        }
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
            User.Gender = SelectedGender.Item;
            User.MaritalStatus = SelectedMarital.Item;
            User.Education = SelectedEducation.Item;
            if (Age>2)
            {
                User.YearBorn = Age.FromYearsOld();
            }
            UserManager.AddUser(User);
            HostScreen.Router.NavigateBack.Execute();
        }

        public override string ToString() => "AddUserVM";
    }
}
