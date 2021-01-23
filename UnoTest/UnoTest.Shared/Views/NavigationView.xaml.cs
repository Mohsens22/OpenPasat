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
using System.Diagnostics;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using UnoTest.Shared.UserModels;
#if NETFX_CORE
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
#endif


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

            this.WhenActivated(d =>
            {
                NavView.Loaded += NavView_Loaded;
                NavView.BackRequested += NavView_BackRequested;
                NavView.ItemInvoked += NavView_ItemInvoked;
            });
            
            
#if NETFX_CORE
            CustomieTitleBar();
#endif
        }

        private void NavView_ItemInvoked(Windows.UI.Xaml.Controls.NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item= (MenuItem)args.InvokedItem;
            if (item != ViewModel.SelectedNavigationItem)
            {
                ViewModel.Navigate(item);
            }
            
        }

        private void NavView_BackRequested(Windows.UI.Xaml.Controls.NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            ViewModel.NavigateBack();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Navigate(((IEnumerable<MenuItem>)NavView.MenuItemsSource).ElementAt(0));
            
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
        }

#endif
    }
}
