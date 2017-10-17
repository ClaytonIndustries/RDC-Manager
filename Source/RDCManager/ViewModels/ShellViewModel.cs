using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Messages;

namespace RDCManager.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<RDCSelectedMessage>
    {
        public IScreen RDCSessionVM
        {
            get;
            private set;
        }

        public IScreen RDCCollectionVM
        {
            get;
            private set;
        }

        public IScreen UserAccountsVM
        {
            get;
            private set;
        }

        public IScreen RDCGroupsVM
        {
            get;
            private set;
        }

        public ISnackbarMessageQueue SnackbarMessageQueue
        {
            get; set;
        }

        private readonly IEventAggregator _events;

        public ShellViewModel(IEventAggregator events, ISnackbarMessageQueue snackbarMessageQueue, RDCSessionViewModel rdcSessionVM, RDCCollectionViewModel rdcCollectionVM, 
            RDCUserAccountsViewModel userAccountsVM, RDCGroupsViewModel rdcGroupsVM)
        {
            DisplayName = "RDC Manager";

            _events = events;
            _events.Subscribe(this);

            SnackbarMessageQueue = snackbarMessageQueue;

            RDCSessionVM = rdcSessionVM;
            RDCCollectionVM = rdcCollectionVM;
            UserAccountsVM = userAccountsVM;
            RDCGroupsVM = rdcGroupsVM;

            ShowRDCList();
        }

        public void ShowRDCList()
        {
            if (!(ActiveItem is RDCCollectionViewModel))
            {
                ChangeActiveItem(RDCCollectionVM, false);
            }
        }

        public void ShowRDCSession()
        {
            if (!(ActiveItem is RDCSessionViewModel))
            {
                ChangeActiveItem(RDCSessionVM, false);
            }
        }

        public void StopCurrentRDC()
        {
            _events.PublishOnUIThread(new StopRDCMessage());
        }

        public void ShowUserAccounts()
        {
            if (!(ActiveItem is RDCUserAccountsViewModel))
            {
                ChangeActiveItem(UserAccountsVM, false);
            }
        }

        public void ShowRDCGroups()
        {
            if (!(ActiveItem is RDCGroupsViewModel))
            {
                ChangeActiveItem(RDCGroupsVM, false);
            }
        }

        public void Handle(RDCSelectedMessage message)
        {
            ShowRDCSession();
        }
    }
}