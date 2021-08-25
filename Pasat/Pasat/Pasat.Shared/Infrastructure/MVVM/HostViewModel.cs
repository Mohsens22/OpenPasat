using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pasat.ViewModels
{
    public abstract class HostViewModel : ViewModelBase, IScreen
    {
        protected HostViewModel()
        {
            Router = new RoutingState();
        }
        [Reactive]
        public RoutingState Router { get; protected set; }
    }
}
