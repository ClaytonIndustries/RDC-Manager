using System.Collections.Generic;

namespace RDCManager.Models
{
    public interface IUserAccountManager
    {
        IEnumerable<UserAccount> GetUserAccounts();
        void Save();
        UserAccount CreateNew();
    }
}