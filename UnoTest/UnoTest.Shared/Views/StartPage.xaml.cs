using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoTest.Shared.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page,IViewFor<StartPageViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(StartPageViewModel), typeof(StartPage), null);
        public StartPage()
        {
            this.InitializeComponent();
            ViewModel = new StartPageViewModel();
            this.WhenActivated(
                disposables => {
                this
                    .Events().KeyDown
                    .Select(args=>args)
                    .InvokeCommand(this, x => x.ViewModel.Refresh)
                    .DisposeWith(disposables);


                });
        }

        public StartPageViewModel ViewModel {
            get => (StartPageViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        object IViewFor.ViewModel 
        {
            get => ViewModel;
            set => ViewModel = (StartPageViewModel)value;
        }
    }
}
