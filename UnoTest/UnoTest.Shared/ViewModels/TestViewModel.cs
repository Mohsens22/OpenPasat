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

namespace UnoTest.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestViewModel: RoutableViewModel
    {

        public TestViewModel(IScreen screen, TestIndentifier identifier, User selectedUser) :base(screen)
        {
            User = selectedUser;
            ActiveIdentifier = identifier;
            identifier.Load();
            FirstButtonCommand = ReactiveCommand.Create(FirstButtonAction);
            SecondButtonCommand = ReactiveCommand.Create(SecondButtonAction);
            ThirdButtonCommand = ReactiveCommand.Create(ThirdButtonAction);
            FourthButtonCommand = ReactiveCommand.Create(FourthButtonAction);


        }

        private static readonly Random _rnd = new Random();
        //OnPageLoad
        public async Task Updater(CancellationToken token)
        {
            ActiveIdentifier.StartTime = Now();
            for (int i = 0; i < ActiveIdentifier.TestFragments.Count; i++)
            {
                if (token.IsCancellationRequested)
                    return;

                ProgressPercentage = (i * 100) / ActiveIdentifier.TestCount;
                IsResultTaken = false;
                InctiveFragment = ActiveFragment;
                ActiveFragment = ActiveIdentifier.TestFragments[i];
                if (ActiveIdentifier.IsAudioEnabled)
                {
                    var audioPlayer = Locator.Current.GetService<IMediaPlayer>();
                    audioPlayer.Play(ActiveFragment.Number);
                }
                
                ActiveFragment.RepresentationTime = Now();
                await Task.Delay(ActiveIdentifier.ImpulseRate);

                if (ActiveFragment.PreviousAnswer.HasValue)
                {
                    SetAnswers();
                    CanInput = true;
                }
                await Task.Delay(ActiveIdentifier.AnswerTime);
                CanInput = false;
                if (ActiveFragment.PreviousAnswer.HasValue && !IsResultTaken)
                {
                    var answer = TestAnswer.NotAnswered(ActiveFragment, InctiveFragment);
                    ActiveIdentifier.Answers.Add(answer);
                    if (ActiveIdentifier.Correction)
                    {
                        LastAnswerStatus = answer.Status;
                    }
                    
                }
                

            }
            ActiveIdentifier.EndTime = Now();
            TestManager.InsetTest(ActiveIdentifier);
            ActiveIdentifier.User = User;



            

            HostScreen.Router.NavigateAndReset.Execute(new ResultsViewModel(HostScreen, ActiveIdentifier,true));
        }

        private void SetAnswers()
        {
            AnswerKey = _rnd.Next(1, 5);
            var ans = ActiveFragment.CloseAnswers.Split(" ").Select(x=>x.To<int>()).ToArray();
            FirstButton = new KeyValuePair<int, bool>(ans[0],false);
            SecondButton = new KeyValuePair<int, bool>(ans[1], false);
            ThirdButton = new KeyValuePair<int, bool>(ans[2], false);
            FourthButton = new KeyValuePair<int, bool>(ans[3], false);

            var correct = new KeyValuePair<int, bool>(ActiveFragment.PreviousAnswer.Value, true); 
            switch (AnswerKey)
            {
                case 1:
                    FirstButton = correct;
                    break;
                case 2:
                    SecondButton = correct;
                    break;
                case 3:
                    ThirdButton = correct;
                    break;
                case 4:
                    FourthButton = correct;
                    break;
                default:
                    break;
            }
        }

        public User User { get; set; }

        [Reactive]
        public CorrectionStatus LastAnswerStatus { get; set; }

        [Reactive]
        public bool IsResultTaken { get; set; }
        [Reactive]
        public bool CanInput { get; set; }
        [Reactive]
        public int ProgressPercentage { get; set; }
        [Reactive]
        public int AnswerKey { get; set; }

        public TestIndentifier ActiveIdentifier { get; set; }
        [Reactive]
        public TestFragment ActiveFragment { get; set; }
        public TestFragment InctiveFragment { get; set; }
        [Reactive]
        public KeyValuePair<int,bool> FirstButton { get; set; }
        [Reactive]
        public KeyValuePair<int, bool> ThirdButton { get; set; }
        [Reactive]
        public KeyValuePair<int, bool> SecondButton { get; set; }
        [Reactive]
        public KeyValuePair<int, bool> FourthButton { get; set; }


        public ReactiveCommand<Unit, Unit> FirstButtonCommand { get; set; }
        public ReactiveCommand<Unit, Unit> SecondButtonCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ThirdButtonCommand { get; set; }
        public ReactiveCommand<Unit, Unit> FourthButtonCommand { get; set; }

        private void FirstButtonAction() => Entry(FirstButton.Key, InputType.UI);
        private void SecondButtonAction() => Entry(SecondButton.Key, InputType.UI);
        private void ThirdButtonAction() => Entry(ThirdButton.Key, InputType.UI);
        private void FourthButtonAction() => Entry(FourthButton.Key, InputType.UI);

        public void Entry(int num,InputType type)
        {
            CanInput = false;
            IsResultTaken = true;
            var answer = TestAnswer.Answer(ActiveFragment, InctiveFragment, num, Now(), type);
            ActiveIdentifier.Answers.Add(answer);
            if (ActiveIdentifier.Correction)
            {
                LastAnswerStatus = answer.Status;
            }
        }
        DateTimeOffset Now() => DateTimeOffset.UtcNow;
        public override string ToString() => "TestVM";
    }
}
