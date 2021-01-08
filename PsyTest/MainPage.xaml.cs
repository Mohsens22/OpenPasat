using Olive;
using PsyTest.Model;
using PsyTest.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Media.Render;
using Windows.Media.Transcoding;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PsyTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;
            if (testStarted && canInput)
            {
                switch (args.VirtualKey)
                {
                    case VirtualKey.Up:
                        first_Click(first, null);
                        break;
                    case VirtualKey.Left:
                        first_Click(second, null);
                        break;
                    case VirtualKey.Down:
                        first_Click(third, null);
                        break;
                    case VirtualKey.Right:
                        first_Click(fourth, null);
                        break;
                    default:
                        break;
                }
            }
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            int wait = millisec.Text.To<int>();
            int comp = compulse.Text.To<int>();
            cnt = countTest.Text.To<int>();

            informatives.Visibility = Visibility.Collapsed;

            items = TestItemService.LoadItems(cnt);
            await Updater(wait-comp,comp);
            informatives.Visibility = Visibility.Visible;

        }
        int cnt;
        List<int> ass = new List<int>();
        int current;
        int correct;
        bool resultTaken;
        Random rnd = new Random();
        TestItem items;

        bool testStarted = false;
        bool canInput = false;
        async Task Updater(int quantum, int comp)
        {
            testStarted = true;
            progress.Visibility = Visibility.Visible;
            progress.Value = 0;
            await AudioPlayer.CreateAudioGraph();
            for (int i = 0; i < items.Items.Count; i++)
            {
                resultTaken = false;
                var item = items.Items[i];
                
                await AudioPlayer.Play(item);
                items.ItemsRepresented.Add(DateTimeOffset.UtcNow);
                await Task.Delay(comp);
                canInput = true;

                if (i>0)
                {
                    correct = items.Results[i - 1];
                    var falses = items.CloseAnswers[i - 1];
                    ShowPanel(falses);
                }

                await Task.Delay(quantum);
                canInput = false;
                if (i > 0 && !resultTaken)
                {
                    items.ResultsTaken.Add(new KeyValuePair<bool, DateTimeOffset>(false, DateTimeOffset.UtcNow));
                    //Clear the passage
                    answers.Visibility = Visibility.Collapsed;
                }
                var nm = (i * 100) / cnt;
                progress.Value = nm;

            }

            result.Text = items.ResultsTaken.Select(x => $"{x.Key} - {x.Value}").ToLinesString() + Environment.NewLine + Environment.NewLine + string.Join(' ',ass);
            testStarted = false;
        }

        private void ShowPanel(List<int> falses)
        {
            current = rnd.Next(1,5);
            ass.Add(current);
            first.Content = falses[0];
            second.Content = falses[1];
            third.Content = falses[2];
            fourth.Content = falses[3];

            switch (current)
            {
                case 1:
                    first.Content = correct;
                    break;
                case 2:
                    second.Content = correct;
                    break;
                case 3:
                    third.Content = correct;
                    break;
                case 4:
                    fourth.Content = correct;
                    break;
                default:
                    break;
            }
            answers.Visibility = Visibility.Visible;
        }

        private void first_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var isCorrect = button.Content.ToString().To<int>() == correct;
            items.ResultsTaken.Add(new KeyValuePair<bool, DateTimeOffset>(isCorrect, DateTimeOffset.UtcNow));
            resultTaken = true;
            answers.Visibility = Visibility.Collapsed;
            
        }
    }
}
