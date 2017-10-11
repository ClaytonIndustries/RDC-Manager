using System.Collections.Generic;

namespace RDCManager.Models
{
    public interface IRDCInstanceManager
    {
        IEnumerable<RDC> GetRDCs();
        void Save();
    }
}