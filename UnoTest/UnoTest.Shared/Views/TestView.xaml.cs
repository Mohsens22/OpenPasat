using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnoTest.Shared.Models;
using UnoTest.Shared.ViewModels;
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

namespace UnoTest.Shared.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestView : Page,IViewFor<TestViewModel>
    {
        public TestView()
        {
            this.InitializeComponent();
            
            this.WhenActivated(async disposables =>
            {
                IsActivated = true;
                Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
                await ViewModel.Updater();
            });
            
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
        }
        bool IsActivated;
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            args.Handled = true;
            
            if (IsActivated && ViewModel.CanInput)
            {
                switch (args.VirtualKey)
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

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
           .Register(nameof(ViewModel), typeof(TestViewModel), typeof(TestView), null);
        public TestViewModel ViewModel
        {
            get => (TestViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TestViewModel)value;
        }
    }
}
