using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Pasat.Models;
using Pasat.ViewModels;
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

namespace Pasat.Views
{
    public abstract partial class ValidationViewBase : AppReactivePage<ValidationViewModel>
    {

    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ValidationView : ValidationViewBase
    {
        public ValidationView()
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

                var tokenSource = new CancellationTokenSource();

                Disposable
                .Create(() =>
                {
                    tokenSource.Cancel();
                })
                .DisposeWith(disposables);
                await ViewModel.Updater(tokenSource.Token);
            });
        }
        bool IsActivated;
        private void StackPanel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;

            if (IsActivated && ViewModel.CanInput)
            {
                if (e.Key == VirtualKey.Up|| e.Key == VirtualKey.Down || e.Key == VirtualKey.Left || e.Key == VirtualKey.Right)
                {
                    ViewModel.Entry(e.Key, InputType.Physical);
                }
            }
        }
    }
}
