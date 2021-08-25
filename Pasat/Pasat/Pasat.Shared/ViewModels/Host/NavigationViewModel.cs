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
using Pasat.UserModels;
using Pasat.Extentions;

namespace Pasat.ViewModels
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

				GoBack = ReactiveCommand.Create(BackHandler);

				NavigationItems = new List<MenuItem>();
				NavigationItems.Add(new MenuItem(new TestHostViewModel(), LanguageHelper.GetString("Test", "Text") ));
                if (App.Features.InAppDatabase==Infrastructure.Features.FeatureAvailability.Available)
                {
					if (App.Features.MultiUserEnabled == Infrastructure.Features.FeatureAvailability.Unavailable)
					{
						NavigationItems.Add(new MenuItem(new UserHostViewModel("public"), LanguageHelper.GetString("History", "Text")));
					}
                    else
                    {
						NavigationItems.Add(new MenuItem(new UserHostViewModel(), LanguageHelper.GetString("Users","Text")));
					}
					
				}
				NavigationItems.Add(new MenuItem(new AboutHostViewModel(), LanguageHelper.GetString("About", "Text")));
                foreach (var item in NavigationItems)
                {
                    item.ViewModelType.Router.NavigationStack.CollectionChanged += (s,e)=> 
					{
						var objects = s as ObservableCollection<IRoutableViewModel>;
						if (SelectedNavigationItem == item)
                        {
							IsBackEnabled = objects.Count > 1;
						}
					};
                }


			});
		}

		void BackHandler()
        {
			SelectedNavigationItem.ViewModelType.Router.NavigateBack.Execute();
        }

        

        [Reactive]
		public bool CanGoBack { get; set; }
		[Reactive]
		public List<MenuItem> NavigationItems { get; private set; }
		[Reactive]
		public ReactiveCommand<Unit,Unit> GoBack { get; set; }

        [Reactive]
		public MenuItem SelectedNavigationItem { get; set; }
		[Reactive]
		public bool IsBackEnabled { get; set; }

		public override string ToString() => "NavigationVM";
    }
}
