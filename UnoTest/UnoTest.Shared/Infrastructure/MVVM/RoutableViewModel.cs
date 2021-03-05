using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;

namespace UnoTest.ViewModels
{
    public abstract class RoutableViewModel : ViewModelBase, IRoutableViewModel, IValidatableViewModel
    {
        protected RoutableViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment { get => this.ToString(); }
        public IScreen HostScreen { get; protected set; }

        public ValidationContext ValidationContext { get; } = new ValidationContext();
    }
}
