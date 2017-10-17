using System.Collections.ObjectModel;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Controls;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCUserAccountsViewModel : Screen
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

        public RDCUserAccountsViewModel(ISnackbarMessageQueue snackbarMessageQueue, IUserAccountManager userAccountManager)
        {
            _snackbarMessageQueue = snackbarMessageQueue;
            _userAccountManager = userAccountManager;

            Accounts = new ObservableCollection<UserAccount>(_userAccountManager.GetUserAccounts());
        }

        public void New()
        {
            Accounts.Add(_userAccountManager.CreateNew());
        }

        public void Save()
        {
            string saveMessage = _userAccountManager.Save() ? "Changes saved" : "Failed to save";

            _snackbarMessageQueue.Enqueue(saveMessage);
        }

        public async void Delete()
        {
            if (SelectedAccount != null)
            {
                object result = await DialogHost.Show(new Dialog());

                if((bool)result)
                {
                    _userAccountManager.Delete(SelectedAccount);
                    _userAccountManager.Save();

                    Accounts.Remove(SelectedAccount);

                    _snackbarMessageQueue.Enqueue("User Account deleted and changes saved");
                }
            }
        }
    }
}