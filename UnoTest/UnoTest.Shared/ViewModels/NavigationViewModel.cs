using Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using UnoTest.Shared.UserModels;

namespace UnoTest.Shared.ViewModels
{
	[Windows.UI.Xaml.Data.Bindable]
	public class NavigationViewModel : ViewModelBase, IScreen, IActivatableViewModel
	{
		public NavigationViewModel()
		{
			Router = new RoutingState();
			this.Router.NavigationStack.CollectionChanged += NavigationStack_CollectionChanged;
		}

		public void Navigate(MenuItem item)
        {
			Router.Navigate.Execute((IRoutableViewModel)App.Container.Resolve(item.ViewModelType));
			SelectedNavigationItem = item;
        }
        private void NavigationStack_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
			CanGoBack = Router.NavigationStack.Count > 1;
        }
		public void NavigateBack()
        {
			Router.NavigateBack.Execute();;
		}
		[Reactive]
		public bool CanGoBack { get; set; }
		public IReadOnlyList<MenuItem> NavigationItems => new List<MenuItem>
		{
			new MenuItem(typeof(StartUpViewModel), "Test", "Home"),
			new MenuItem(typeof(AboutViewModel), "About", "Home")
		}.AsReadOnly();

		[Reactive]
		public MenuItem SelectedNavigationItem { get; set; }
		[Reactive]
		public RoutingState Router { get; set; }
		[Reactive]
		public bool IsBackEnabled { get; set; }
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
	}
}
