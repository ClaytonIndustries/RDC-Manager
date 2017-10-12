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

        public RDCSettingsViewModel()
        {
            Accounts = new ObservableCollection<UserAccount>();
        }

        public void NewAccount()
        {
            Accounts.Add(new UserAccount());
        }
    }
}