using System.Collections.Generic;

namespace RDCManager.Models
{
    public interface IRDCGroupManager
    {
        IEnumerable<RDCGroup> GetGroups();
        RDCGroup CreateNew();
        void Delete(RDCGroup rdcGroup);
        bool Save();
    }
}