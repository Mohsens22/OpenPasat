using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Pasat.ViewModels;

namespace Pasat.UserModels
{
	[Windows.UI.Xaml.Data.Bindable]
	public class MenuItem
	{
		public MenuItem(IScreen viewModelType, string title)
		{
			ViewModelType = viewModelType;
			Title = title;
		}

		public IScreen ViewModelType { get; }

		public string Title { get; }

		public override string ToString() => this.Title;
    }
}
