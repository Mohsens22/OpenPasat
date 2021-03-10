using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Extentions;
using UnoTest.Logic;
using UnoTest.Models;
using UnoTest.Services;
using Olive;
using Windows.UI.Xaml.Input;
using System.Text.Json;
using System.Threading;
using UnoTest.Shared.Logic;
using Windows.System;

namespace UnoTest.ViewModels
{
    public class ValidationViewModel: RoutableViewModel
    {

        public ValidationViewModel(IScreen screen, TestIndentifier identifier, Models.User selectedUser,bool proceedOnInvalid = false) : base(screen)
        {
            User = selectedUser;

            FirstButtonCommand = ReactiveCommand.Create(FirstButtonAction);
            SecondButtonCommand = ReactiveCommand.Create(SecondButtonAction);
            ThirdButtonCommand = ReactiveCommand.Create(ThirdButtonAction);
            FourthButtonCommand = ReactiveCommand.Create(FourthButtonAction);


        }

        private static readonly Random _rnd = new Random();
        //OnPageLoad
        public async Task Updater(CancellationToken token)
        {

        }
        [Reactive]
        public CorrectionStatus LastAnswerStatus { get; set; }

        [Reactive]
        public bool IsResultTaken { get; set; }

        public Models.User User { get; set; }

        [Reactive]
        public bool CanInput { get; set; }

        [Reactive]
        public int ProgressPercentage { get; set; }

        public ReactiveCommand<Unit, Unit> FirstButtonCommand { get; set; }
        public ReactiveCommand<Unit, Unit> SecondButtonCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ThirdButtonCommand { get; set; }
        public ReactiveCommand<Unit, Unit> FourthButtonCommand { get; set; }

        private void FirstButtonAction() => Entry(VirtualKey.Up, InputType.UI);
        private void SecondButtonAction() => Entry(VirtualKey.Left, InputType.UI);
        private void ThirdButtonAction() => Entry(VirtualKey.Down, InputType.UI);
        private void FourthButtonAction() => Entry(VirtualKey.Right, InputType.UI);

        public TestIndentifier ActiveIdentifier { get; set; }

        public void Entry(VirtualKey key, InputType type)
        {
            CanInput = false;
            IsResultTaken = true;

            Debug.WriteLine(key.ToString() + " " + type.ToString());
        }
        DateTimeOffset Now() => DateTimeOffset.UtcNow;

        public override string ToString() => "ValidationVM";
    }
}
