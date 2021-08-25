using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Pasat.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Pasat.UserModels;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
#if NETFX_CORE
using Windows.ApplicationModel.Core;
#endif


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pasat.Views
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
            this.ViewModel = new NavigationViewModel();
            this.DataContext = ViewModel;
            this.InitializeComponent();

            this.WhenActivated(d =>
            {
            });

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
#if NETFX_CORE
            CustomieTitleBar();
#endif
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ViewModel.CanGoBack)
            {
                ViewModel.GoBack.Execute();
            }
        }



#if NETFX_CORE
        private void CustomieTitleBar()
        {
            // using Windows.UI.ViewManagement;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;

            // Set inactive window colors
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);

            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(AppTitleBar);

            // Register a handler for when the size of the overlaid caption control changes.
            // For example, when the app moves to a screen with a different DPI.
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

        }
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Get the size of the caption controls area and back button 
            // (returned in logical pixels), and move your content around as necessary.
            LeftPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayLeftInset);
            RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);

            // Update title bar control size as needed to account for system size changes.
            AppTitleBar.Height = coreTitleBar.Height;
        }

#endif
    }
}
