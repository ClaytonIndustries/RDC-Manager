using System;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCSessionViewModel : Screen, IHandle<RDCSelectedMessage>, IHandle<StopRDCMessage>
    {
        private RDC _selectedRDC;
        public RDC SelectedRDC
        {
            get { return _selectedRDC; }
            set
            {
                _selectedRDC = value;
                NotifyOfPropertyChange(() => SelectedRDC);
                NotifyOfPropertyChange(() => NoSelectedRDC);
                NotifyOfPropertyChange(() => StoppedRDC);
            }
        }

        private ObservableCollection<UserAccount> _userAccounts;
        public ObservableCollection<UserAccount> UserAccounts
        {
            get { return _userAccounts; }
            set { _userAccounts = value; NotifyOfPropertyChange(() => UserAccounts); }
        }

        private UserAccount _selectedUserAccount;
        public UserAccount SelectedUserAccount
        {
            get { return _selectedUserAccount; }
            set
            {
                _selectedUserAccount = value;
                SelectedUserAccountChanged();
                NotifyOfPropertyChange(() => SelectedUserAccount);
                NotifyOfPropertyChange(() => ManualUserAccountEntryEnabled);
            }
        }

        public bool NoSelectedRDC
        {
            get { return SelectedRDC == null; }
        }

        public bool StoppedRDC
        {
            get { return SelectedRDC != null && !SelectedRDC.IsRunning; }
        }

        public bool ManualUserAccountEntryEnabled
        {
            get
            {
                return SelectedUserAccount != null && SelectedUserAccount.Name == MANUAL_ENTRY_NAME;
            }
        }

        private readonly IEventAggregator _events;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IRDCInstanceManager _rdcInstanceManager;
        private readonly IUserAccountManager _userAccountManager;

        private const string MANUAL_ENTRY_NAME = "Manual Entry";

        public RDCSessionViewModel(IEventAggregator events, ISnackbarMessageQueue snackbarMessageQueue, IRDCInstanceManager rdcInstanceManager, IUserAccountManager userAccountManager)
        {
            _events = events;
            _events.Subscribe(this);

            _snackbarMessageQueue = snackbarMessageQueue;
            _rdcInstanceManager = rdcInstanceManager;
            _userAccountManager = userAccountManager;
        }

        protected override void OnActivate()
        {
            if (SelectedRDC != null)
            {
                UserAccounts = new ObservableCollection<UserAccount>(_userAccountManager.GetUserAccounts());
                UserAccounts.Insert(0, new UserAccount() { Name = MANUAL_ENTRY_NAME });

                UserAccount userAccount = UserAccounts.FirstOrDefault(x => x.Id == SelectedRDC.UserAccountId);

                if (userAccount != null)
                {
                    SelectedUserAccount = userAccount;
                }
                else
                {
                    if (SelectedRDC.UserAccountId != Guid.Empty)
                    {
                        SelectedRDC.UserAccountId = Guid.Empty;
                        SelectedRDC.Username = string.Empty;
                        SelectedRDC.Password = string.Empty;
                    }

                    SelectedUserAccount = UserAccounts.First();
                }
            }
        }

        public void Start()
        {
            if (SelectedRDC != null && !SelectedRDC.IsRunning)
            {
                SelectedRDC.Connect();
            }
        }

        public void Save()
        {
            string saveMessage = _rdcInstanceManager.Save() ? "Changes saved" : "Failed to save";

            _snackbarMessageQueue.Enqueue(saveMessage);
        }

        public void Delete()
        {
            if (SelectedRDC != null)
            {
                _rdcInstanceManager.Delete(SelectedRDC);
                _rdcInstanceManager.Save();

                SelectedRDC = null;

                _snackbarMessageQueue.Enqueue("RDC deleted");
            }
        }

        public void Handle(RDCSelectedMessage message)
        {
            if (message.SelectedRDC != null)
            {
                SelectedRDC = message.SelectedRDC;
            }
        }

        public void Handle(StopRDCMessage message)
        {
            if (SelectedRDC != null && SelectedRDC.IsRunning)
            {
                SelectedRDC.Disconnect();
            }
        }

        private void SelectedUserAccountChanged()
        {
            if (SelectedUserAccount != null)
            {
                SelectedRDC.UserAccountId = SelectedUserAccount.Name == MANUAL_ENTRY_NAME ? Guid.Empty : SelectedUserAccount.Id;
                SelectedRDC.Username = SelectedUserAccount.Username;
                SelectedRDC.Password = SelectedUserAccount.Password;
            }
        }
    }
}
