﻿using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Shared.Extentions;
using UnoTest.Shared.Logic;
using UnoTest.Shared.Models;
using UnoTest.Shared.Services;
using Windows.System;
using Windows.UI.Xaml.Input;

namespace UnoTest.Shared.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class TestViewModel: RoutableViewModel
    {

        public TestViewModel(IScreen screen, TestIndentifier identifier):base(screen)
        {
            ActiveIdentifier = identifier;
            identifier.Load();
            FirstButtonCommand = ReactiveCommand.Create(FirstButtonAction);
            SecondButtonCommand = ReactiveCommand.Create(SecondButtonAction);
            ThirdButtonCommand = ReactiveCommand.Create(ThirdButtonAction);
            FourthButtonCommand = ReactiveCommand.Create(FourthButtonAction);


        }

        private static readonly Random _rnd = new Random();
        //OnPageLoad
        public async Task Updater()
        {
            ActiveIdentifier.StartTime = Now();
            for (int i = 0; i < ActiveIdentifier.TestFragments.Count; i++)
            {
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

#if DEBUG
            JsonConvert.SerializeObject(ActiveIdentifier).CopyToClipboard();
#endif

            HostScreen.Router.Navigate.Execute(new ResultsViewModel(HostScreen, ActiveIdentifier));
        }

        private void SetAnswers()
        {
            AnswerKey = _rnd.Next(1, 5);

            FirstButton = new KeyValuePair<int, bool>(ActiveFragment.CloseAnswers[0],false);
            SecondButton = new KeyValuePair<int, bool>(ActiveFragment.CloseAnswers[1], false);
            ThirdButton = new KeyValuePair<int, bool>(ActiveFragment.CloseAnswers[2], false);
            FourthButton = new KeyValuePair<int, bool>(ActiveFragment.CloseAnswers[3], false);

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
