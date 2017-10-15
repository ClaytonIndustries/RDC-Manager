using System;
using System.Collections.Generic;
using System.Linq;

namespace RDCManager.Models
{
    public class UserAccountManager : IUserAccountManager
    {
        private readonly IFileAccess _fileAccess;
        private readonly IEncryptionManager _encryptionManager;

        private ICollection<UserAccount> _userAccounts;

        public UserAccountManager(IFileAccess fileAccess, IEncryptionManager encryptionManager)
        {
            _fileAccess = fileAccess;
            _encryptionManager = encryptionManager;

            Load();
        }

        public IEnumerable<UserAccount> GetUserAccounts()
        {
            return _userAccounts;
        }

        public UserAccount CreateNew()
        {
            UserAccount userAccount = new UserAccount()
            {
                Id = Guid.NewGuid()
            };

            _userAccounts.Add(userAccount);

            return userAccount;
        }

        public void Delete(UserAccount userAccount)
        {
            _userAccounts.Remove(userAccount);
        }

        public bool Save()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCAccounts.json";

                IEnumerable<UserAccount> encrypted = _userAccounts.Select(x => new UserAccount()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Username = x.Username,
                    Password = _encryptionManager.AesEncrypt(x.Password),
                    Domain = x.Domain
                });

                _fileAccess.Write(saveLocation, encrypted);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Load()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCAccounts.json";

                IEnumerable<UserAccount> encrypted = _fileAccess.Read<IEnumerable<UserAccount>>(saveLocation);

                _userAccounts = encrypted.Select(x => new UserAccount()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Username = x.Username,
                    Password = _encryptionManager.AesDecrypt(x.Password),
                    Domain = x.Domain
                }).ToList();
            }
            catch
            {
                _userAccounts = new List<UserAccount>();
            }
        }
    }
}