using System.Collections.Generic;

namespace RDCManager.Models
{
    public interface IUserAccountManager
    {
        IEnumerable<UserAccount> GetUserAccounts();
        bool Save();
        UserAccount CreateNew();
        void Delete(UserAccount userAccount);
    }
}