using System;
using System.Collections.Generic;

namespace RDCManager.Models
{
    public class UserAccountManager : IUserAccountManager
    {
        private readonly IFileAccess _fileAccess;

        private List<UserAccount> _userAccounts;

        public UserAccountManager(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;

            Load();
        }

        public IEnumerable<UserAccount> GetUserAccounts()
        {
            return _userAccounts;
        }

        public UserAccount CreateNew()
        {
            UserAccount userAccount = new UserAccount();

            _userAccounts.Add(userAccount);

            return userAccount;
        }

        public void Delete(UserAccount userAccount)
        {
            _userAccounts.Remove(userAccount);
        }

        public void Save()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCAccounts.xml";

                _fileAccess.Write(saveLocation, _userAccounts);
            }
            catch
            {
            }
        }

        private void Load()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCAccounts.xml";

                _userAccounts = _fileAccess.Read<List<UserAccount>>(saveLocation);
            }
            catch
            {
                _userAccounts = new List<UserAccount>();
            }
        }
    }
}