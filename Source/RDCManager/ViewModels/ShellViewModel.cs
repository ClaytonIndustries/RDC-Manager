using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<RDCSelectedMessage>
    {
        private bool _isFullScreen;
        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set { _isFullScreen = value; NotifyOfPropertyChange(() => IsFullScreen); }
        }

        public IScreen RDCSessionVM { get; }

        public IScreen RDCCollectionVM { get; }

        public IScreen RDCUserAccountsVM { get; }

        public IScreen RDCGroupsVM { get; }

        public ISnackbarMessageQueue SnackbarMessageQueue { get; }

        private readonly IApplicationWrapper _applicationWrapper;
        private readonly IEventAggregator _events;

        public ShellViewModel(IEventAggregator events, ISnackbarMessageQueue snackbarMessageQueue, IApplicationWrapper applicationWrapper, RDCSessionViewModel rdcSessionVM, 
            RDCCollectionViewModel rdcCollectionVM, RDCUserAccountsViewModel rdcUserAccountsVM, RDCGroupsViewModel rdcGroupsVM)
        {
            DisplayName = "RDC Manager";

            _applicationWrapper = applicationWrapper;

            _events = events;
            _events.Subscribe(this);

            SnackbarMessageQueue = snackbarMessageQueue;

            RDCSessionVM = rdcSessionVM;
            RDCCollectionVM = rdcCollectionVM;
            RDCUserAccountsVM = rdcUserAccountsVM;
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

        public void ToggleFullScreen()
        {
            _applicationWrapper.ToggleFullScreen();

            IsFullScreen = !IsFullScreen;
        }

        public void ShowUserAccounts()
        {
            if (!(ActiveItem is RDCUserAccountsViewModel))
            {
                ChangeActiveItem(RDCUserAccountsVM, false);
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