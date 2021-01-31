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
	public class NavigationViewModel : HostViewModel
	{
		public NavigationViewModel():base()
		{
			this.WhenActivated(d =>
			{
				this.WhenAnyValue(x => x.SelectedNavigationItem)
				.WhereNotNull()
				.Select(x => x.ViewModelType.Router.NavigationStack)
				.Subscribe(x => IsBackEnabled = x.Count > 1)
				.DisposeWith(d);
			});
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
		public bool IsBackEnabled { get; set; }

		public override string ToString() => "NavigationVM";
    }
}
