using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		readonly IServiceProvider _ServiceProvider;
		public NavigationViewModel(IServiceProvider serviceProvider)
		{
			_ServiceProvider = serviceProvider;
			SelectedNavigationItem = NavigationItems.First();

			this.WhenActivated(disposables =>
			{
				this
				.WhenAnyValue(vm => vm.SelectedNavigationItem)
				.Select(navItem => navItem.ViewModelType)
				.Select(vmType => (IRoutableViewModel)_ServiceProvider.GetRequiredService(vmType))
				.InvokeCommand(Router.Navigate)
				.DisposeWith(disposables);
			});
		}

		public IReadOnlyList<MenuItem> NavigationItems => new List<MenuItem>
		{
			new MenuItem(typeof(StartUpViewModel), "Home", "Home")
		}.AsReadOnly();

		[Reactive]
		public MenuItem SelectedNavigationItem { get; set; }

		public RoutingState Router { get; } = new RoutingState();
		public ViewModelActivator Activator { get; } = new ViewModelActivator();
	}
}
