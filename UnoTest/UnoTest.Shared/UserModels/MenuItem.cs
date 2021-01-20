using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Shared.UserModels
{
	[Windows.UI.Xaml.Data.Bindable]
	public class MenuItem
	{
		public MenuItem(Type viewModelType, string title, string symbol)
		{
			ViewModelType = viewModelType;
			Title = title;
			Symbol = symbol;
		}

		public Type ViewModelType { get; }

		public string Title { get; }

		public string Symbol { get; }
	}
}
