using ReactiveUI;

namespace UnoTest.ViewModels
{
    public abstract class RoutableViewModel : ViewModelBase, IRoutableViewModel
    {
        protected RoutableViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment { get => this.ToString(); }
        public IScreen HostScreen { get; protected set; }

    }
}
