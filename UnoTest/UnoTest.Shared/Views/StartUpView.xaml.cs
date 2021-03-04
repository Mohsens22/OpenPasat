using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnoTest.Models;
using UnoTest.ViewModels;
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

namespace UnoTest.Views
{

    public abstract partial class StartUpViewBase : AppReactivePage<StartUpViewModel>
    {

    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class StartUpView : StartUpViewBase
    {
        public StartUpView()
        {
            this.InitializeComponent();
            txtAutoComplete.SuggestionChosen += TxtAutoComplete_SuggestionChosen;

            this.WhenActivated(d =>
            {
                this.BindValidation(ViewModel, view => view.Errors.Text);
            });
        }

        private void TxtAutoComplete_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var item = (User)args.SelectedItem;
            ViewModel.SelectedUser = item;
        }
    }

}
