using System.Collections.Generic;

namespace RDCManager.Models
{
    public interface IRDCInstanceManager
    {
        IEnumerable<RDC> GetRDCs();
        void Save();
        RDC CreateNew();
        void Delete(RDC rdc);
    }
}