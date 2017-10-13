using System.Collections.ObjectModel;
using Caliburn.Micro;
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

        private readonly IUserAccountManager _userAccountManager;

        public RDCSettingsViewModel(IUserAccountManager userAccountManager)
        {
            _userAccountManager = userAccountManager;

            Accounts = new ObservableCollection<UserAccount>(_userAccountManager.GetUserAccounts());
        }

        public void NewAccount()
        {
            Accounts.Add(_userAccountManager.CreateNew());
        }

        public void Save()
        {
            _userAccountManager.Save();
        }

        public void Delete()
        {
            if(SelectedAccount != null)
            {
                _userAccountManager.Delete(SelectedAccount);
                _userAccountManager.Save();

                Accounts.Remove(SelectedAccount);
            }
        }
    }
}