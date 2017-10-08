using System.Diagnostics;

namespace RDCManager.Models
{
    public class RDCStarter : IRDCStarter
    {
        public Process StartRDCSession(string machineName)
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "mstsc.exe",
                WindowStyle = ProcessWindowStyle.Maximized,
                Arguments = string.Format("/v {0}", machineName)
            };

            return Process.Start(info);
        }
    }
}