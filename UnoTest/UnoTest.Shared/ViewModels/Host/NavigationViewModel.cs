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
using System.Threading.Tasks;
using UnoTest.UserModels;

namespace UnoTest.ViewModels
{
	[Windows.UI.Xaml.Data.Bindable]
	public class NavigationViewModel : HostViewModel
	{
		public NavigationViewModel():base()
		{
			this.WhenActivated(async d =>
			{
				this.WhenAnyValue(x => x.SelectedNavigationItem)
				.WhereNotNull()
				.Select(x => x.ViewModelType.Router.NavigationStack)
				.Subscribe(x => IsBackEnabled = x.Count > 1)
				.DisposeWith(d);
#if __ANDROID__ || __WASM__ || __SKIA__
				await Task.Delay(10);
#endif

				NavigationItems = new List<MenuItem>
				{
					new MenuItem(new TestHostViewModel(), "Test"),
					new MenuItem(new AboutHostViewModel(), "About")
				};


			});
		}

		
		[Reactive]
		public bool CanGoBack { get; set; }
		[Reactive]
		public IReadOnlyList<MenuItem> NavigationItems { get; private set; }

		[Reactive]
		public MenuItem SelectedNavigationItem { get; set; }
		[Reactive]
		public bool IsBackEnabled { get; set; }

		public override string ToString() => "NavigationVM";
    }
}
