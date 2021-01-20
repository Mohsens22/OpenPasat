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
#if NETFX_CORE
                Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
#endif
                await ViewModel.Updater();
            });

        }
        bool IsActivated;
#if NETFX_CORE
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
#endif


    }
}
