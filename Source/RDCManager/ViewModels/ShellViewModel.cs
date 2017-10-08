using Caliburn.Micro;

namespace RDCManager.ViewModels
{
    public class ShellViewModel : Screen
    {
        public IScreen RDCListVM
        {
            get;
            private set;
        }

        public ShellViewModel(RDCListViewModel rdcListVM)
        {
            DisplayName = "RDC Manager";

            RDCListVM = rdcListVM;
        }

        protected override void OnActivate()
        {
            RDCListVM.Activate();
        }

        protected override void OnDeactivate(bool close)
        {
            RDCListVM.Deactivate(close);
        }
    }
}