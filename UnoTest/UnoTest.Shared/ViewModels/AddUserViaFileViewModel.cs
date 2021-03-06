using Newtonsoft.Json;
using Olive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Text;
using UnoTest.Models;

namespace UnoTest.ViewModels
{
    public class AddUserViaFileViewModel : RoutableViewModel
    {
        public AddUserViaFileViewModel(IScreen hostScreen) : base(hostScreen)
        {
            User = new User();
        }
        [Reactive]
        public User User { get; set; }

        public ReactiveCommand<Unit,Unit> LoadFile { get; set; }

        TemporaryFile file = null;
        void FromFile()
        {
            file = new TemporaryFile("json");
            var path = file.FilePath;
            
            File.WriteAllText(path, JsonConvert.SerializeObject(User, Formatting.Indented));
            Process.Start(path);
        }
        public override string ToString() => "AddViaFileVM";
    }
}
