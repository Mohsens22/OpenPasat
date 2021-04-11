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
using UnoTest.Logic;
using Windows.System;

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ValidationViewModel: RoutableViewModel
    {
        bool _proceed;
        public ValidationViewModel(IScreen screen, TestIndentifier identifier, Models.User selectedUser,bool proceedOnInvalid = false) : base(screen)
        {
            User = selectedUser;
            Context = identifier.LoadValidation();
            ActiveIdentifier = identifier;
            _proceed = proceedOnInvalid;

            FirstButtonCommand = ReactiveCommand.Create(FirstButtonAction);
            SecondButtonCommand = ReactiveCommand.Create(SecondButtonAction);
            ThirdButtonCommand = ReactiveCommand.Create(ThirdButtonAction);
            FourthButtonCommand = ReactiveCommand.Create(FourthButtonAction);

            this.WhenAnyValue(x => x.InfoText)
                .Subscribe(y =>
                {
                    IsInformational = y.HasValue();
                    IsButtonShown = y.IsEmpty();
                });


        }
        //102909934900149200181314

        private static readonly Random _rnd = new Random();
        
        public async Task Updater(CancellationToken token)
        {
            InfoText = "Ready";
            await Task.Delay(ActiveIdentifier.Quantum);
            InfoText = "";

            for (int i = 0; i < Context.Items.Count; i++)
            {
                if (token.IsCancellationRequested)
                    return;
                
                ProgressPercentage = (i * 100) / (Context.Items.Count-1);
                IsResultTaken = false;
                ActiveItem = Context.Items[i];
                if (ActiveIdentifier.IsAudioEnabled)
                {
                    var audioPlayer = Locator.Current.GetService<IMediaPlayer>();
                    audioPlayer.Play(GetPlayNumber(ActiveItem.Key));
                }
                if (ActiveIdentifier.IsVisualEnabled)
                {
                    HighLight();
                }
                CanInput = true;
                ActiveItem.RepresentedAt = Now();
                await Task.Delay(ActiveIdentifier.Quantum);
                CanInput = false;
                if (!IsResultTaken)
                {
                    ActiveItem.Correction = CorrectionStatus.NoEntry;
                    LastAnswerStatus = ActiveItem.Correction;
                    Delight();
                }
            }

            Context.Validate();

            
            if (Context.IsTestValid || _proceed)
            {
                InfoText = "Validation completed. Starting test.";
                await Task.Delay(ActiveIdentifier.Quantum);
                ActiveIdentifier.ValidationContext = Context;
                HostScreen.Router.Navigate.Execute(new TestViewModel(HostScreen,ActiveIdentifier, User));
                // Navigate To Test
                HostScreen.Router.NavigationStack.RemoveAt(HostScreen.Router.NavigationStack.Count - 2);
            }
            else
            {
                InfoText = "Test is invalid. Navigating back";
                ActiveIdentifier.ValidationContext = Context;
                await Task.Delay(5000);
                HostScreen.Router.NavigateAndReset.Execute(new StartUpViewModel(HostScreen));
            }
        }
        private void Delight()
        {
            IsTopHighlighted = false;
            IsLeftHighlighted = false;
            IsRightHighlighted = false;
            IsButtomHighlighted = false;
        }
        private void HighLight()
        {
            Delight();
            switch (ActiveItem.Key)
            {
                case VirtualKey.Left:
                    IsLeftHighlighted = true;
                    break;
                case VirtualKey.Up:
                    IsTopHighlighted = true;
                    break;
                case VirtualKey.Right:
                    IsRightHighlighted = true;
                    break;
                case VirtualKey.Down:
                    IsButtomHighlighted = true;
                    break;
                default:
                    break;
            }
        }

        private int GetPlayNumber(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Left:
                    return 202; 
                case VirtualKey.Up:
                    return 200;
                case VirtualKey.Right:
                    return 203;
                case VirtualKey.Down:
                    return 201;
                default:
                    return 200;
            }
        }

        [Reactive]
        public ValidationItem ActiveItem { get; set; }

        [Reactive]
        public bool IsLeftHighlighted { get; set; }
        [Reactive]
        public bool IsRightHighlighted { get; set; }
        [Reactive]
        public bool IsTopHighlighted { get; set; }
        [Reactive]
        public bool IsButtomHighlighted { get; set; }

        //OnPageLoad
        [Reactive]
        public ValidationContext Context { get; set; }
        [Reactive]
        public CorrectionStatus LastAnswerStatus { get; set; }

        [Reactive]
        public bool IsResultTaken { get; set; }

        public Models.User User { get; set; }

        [Reactive]
        public bool CanInput { get; set; }
        [Reactive]
        public bool IsInformational { get; set; }
        [Reactive]
        public bool IsButtonShown { get; set; }

        [Reactive]
        public string InfoText { get; set; }

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
            ActiveItem.AnsweredAt = Now();
            CanInput = false;
            IsResultTaken = true;

            ActiveItem.InputType = type;

            if (key == ActiveItem.Key)
                ActiveItem.Correction = CorrectionStatus.True;
            else
                ActiveItem.Correction = CorrectionStatus.False;

            ActiveItem.Speed = ActiveItem.AnsweredAt.ToUnixTimeMilliseconds() - ActiveItem.RepresentedAt.ToUnixTimeMilliseconds();
            LastAnswerStatus = ActiveItem.Correction;
            Delight();
        }
        DateTimeOffset Now() => DateTimeOffset.UtcNow;

        public override string ToString() => "ValidationVM";
    }
}
