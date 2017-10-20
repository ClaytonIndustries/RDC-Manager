using System;

namespace RDCManager.Models
{
    public interface IRDCWindow
    {
        event EventHandler Disconnected;
        void Connect(string machineName, string displayName, string username, string password, string domain);
        void Disconnect();
    }
}