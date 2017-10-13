using System.Collections.ObjectModel;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCSettingsViewModel : Screen
    {
        private ObservableCollection<UserAccount> _accounts;
        public ObservableCollection<UserAccount> Accounts
        {
            get { return _accounts; }
            private set { _accounts = value; NotifyOfPropertyChange(() => Accounts); }
        }

        private UserAccount _selectedAccount;
        public UserAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set { _selectedAccount = value; NotifyOfPropertyChange(() => SelectedAccount); }
        }

        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IUserAccountManager _userAccountManager;

        public RDCSettingsViewModel(ISnackbarMessageQueue snackbarMessageQueue, IUserAccountManager userAccountManager)
        {
            _snackbarMessageQueue = snackbarMessageQueue;
            _userAccountManager = userAccountManager;

            Accounts = new ObservableCollection<UserAccount>(_userAccountManager.GetUserAccounts());
        }

        public void NewAccount()
        {
            Accounts.Add(_userAccountManager.CreateNew());
        }

        public void Save()
        {
            string saveMessage = _userAccountManager.Save() ? "Changes saved" : "Failed to save";

            _snackbarMessageQueue.Enqueue(saveMessage);
        }

        public void Delete()
        {
            if(SelectedAccount != null)
            {
                _userAccountManager.Delete(SelectedAccount);
                _userAccountManager.Save();

                Accounts.Remove(SelectedAccount);

                _snackbarMessageQueue.Enqueue("User Account deleted");
            }
        }
    }
}