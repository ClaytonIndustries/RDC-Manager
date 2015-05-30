using System.Diagnostics;

namespace RDCManager.Models
{
    public class RDCStarter : IRDCStarter
    {
        public void StartRDCSession(string machineName)
        {
            Process.Start("mstsc", string.Format("/v {0}", machineName));
        }
    }
}