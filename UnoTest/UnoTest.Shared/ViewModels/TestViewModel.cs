using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnoTest.Shared.Models;
using UnoTest.Shared.Services;

namespace UnoTest.Shared.ViewModels
{
    public class TestViewModel: ReactiveObject, IRoutableViewModel
    {
        public TestViewModel(IScreen screen, TestIndentifier identifier=null)
        {
            HostScreen = screen;
            ActiveIdentifier = identifier;
            ActiveSheet = identifier.Load();
        }
        private readonly Random _rnd = new Random();
        //OnPageLoad
        async Task Updater()
        {
            var audioPlayer = new AudioPlayer();
            ActiveSheet.StartTime = Now();
            for (int i = 0; i < ActiveSheet.TestFragments.Count; i++)
            {
                ActiveFragment = ActiveSheet.TestFragments[i];
                audioPlayer.Play(ActiveFragment.Number);
                ActiveFragment.RepresentationTime = Now();
                await Task.Delay(ActiveSheet.TestInfo.ImpulseRate);

                if (ActiveFragment.PreviousAnswer.HasValue)
                {
                    SetAnswers();
                    CanInput = true;
                }
                await Task.Delay(ActiveSheet.TestInfo.AnswerTime);
                CanInput = false;
                if (ActiveFragment.PreviousAnswer.HasValue && !IsResultTaken)
                {
                    ActiveSheet.Answers.Add(TestAnswer.NotAnswered(ActiveFragment));
                }
                ProgressPercentage = (i * 100) / ActiveSheet.TestInfo.TestCount;

            }
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

        public string UrlPathSegment => this.ToString();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        [Reactive]
        public bool IsResultTaken { get; set; }
        [Reactive]
        public bool CanInput { get; set; }
        [Reactive]
        public int ProgressPercentage { get; set; }
        [Reactive]
        public int AnswerKey { get; set; }

        public TestIndentifier ActiveIdentifier { get; set; }
        public TestFragment ActiveFragment { get; set; }
        public TestSheet ActiveSheet { get; set; }
        [Reactive]
        public KeyValuePair<int,bool> FirstButton { get; set; }
        [Reactive]
        public KeyValuePair<int, bool> ThirdButton { get; set; }
        [Reactive]
        public KeyValuePair<int, bool> SecondButton { get; set; }
        [Reactive]
        public KeyValuePair<int, bool> FourthButton { get; set; }


        DateTimeOffset Now() => DateTimeOffset.UtcNow;
        public override string ToString() => "TestVM";
    }
}
