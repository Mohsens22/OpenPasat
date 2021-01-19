using ReactiveUI;

namespace UnoTest.Shared.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
    }

    public abstract class RoutableViewModel : ViewModelBase, IRoutableViewModel
    {
        protected RoutableViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}
