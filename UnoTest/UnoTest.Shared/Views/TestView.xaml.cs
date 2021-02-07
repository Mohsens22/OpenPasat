using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnoTest.Models;
using UnoTest.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoTest.Views
{
    public abstract partial class TestViewBase : AppReactivePage<TestViewModel>
    {

    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class TestView : TestViewBase
    {
        public TestView()
        {
            this.InitializeComponent();

            this.WhenActivated(async disposables =>
            {
                IsActivated = true;
                ViewModel.WhenAnyValue(x => x.CanInput)
                .Subscribe(b => 
                { 
                    this.Focus(FocusState.Programmatic);
                });

                await ViewModel.Updater();
            });

        }
        bool IsActivated;


        private void StackPanel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;

            if (IsActivated && ViewModel.CanInput)
            {
                switch (e.Key)
                {
                    case VirtualKey.Up:
                        ViewModel.Entry(ViewModel.FirstButton.Key, InputType.Physical);
                        break;
                    case VirtualKey.Left:
                        ViewModel.Entry(ViewModel.SecondButton.Key, InputType.Physical);
                        break;
                    case VirtualKey.Down:
                        ViewModel.Entry(ViewModel.ThirdButton.Key, InputType.Physical);
                        break;
                    case VirtualKey.Right:
                        ViewModel.Entry(ViewModel.FourthButton.Key, InputType.Physical);
                        break;
                    default:
                        break;
                }
            }
        }



    }
}
