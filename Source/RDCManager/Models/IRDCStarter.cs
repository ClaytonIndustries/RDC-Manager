using System.Diagnostics;

namespace RDCManager.Models
{
    public interface IRDCStarter
    {
        Process StartRDCSession(string machineName);
    }
}