using System.Collections.Generic;

namespace RDCManager.Models
{
    public interface IRDCInstanceManager
    {
        IEnumerable<RDC> GetRDCs();
        bool Save();
        RDC CreateNew();
        void Delete(RDC rdc);
    }
}