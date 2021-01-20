using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnoTest.Shared.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI.ViewManagement;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoTest.Shared.Views
{
    public abstract partial class NavigationViewBase : AppReactivePage<NavigationViewModel>
    {
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class NavigationView : NavigationViewBase
    {
        public NavigationView()
        {
            this.InitializeComponent();
#if NETFX_CORE
            CustomieTitleBar();
#endif
        }

#if NETFX_CORE
        private void CustomieTitleBar()
        {
            // using Windows.UI.ViewManagement;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;

            // Set inactive window colors
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
        }
#endif
    }
}
