using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
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
		}

		
		[Reactive]
		public bool CanGoBack { get; set; }
		public IReadOnlyList<MenuItem> NavigationItems => new List<MenuItem>
		{
			new MenuItem(new TestHostViewModel(), "Test"),
			new MenuItem(new AboutHostViewModel(), "About")
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
