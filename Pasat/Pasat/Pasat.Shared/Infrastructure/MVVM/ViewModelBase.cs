using ReactiveUI;

namespace Pasat.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
        public override abstract string ToString();
        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }
    }

}
