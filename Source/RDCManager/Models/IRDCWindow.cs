using System;

namespace RDCManager.Models
{
    public interface IRDCWindow
    {
        event EventHandler Disconnected;
        void Connect(string machineName, string username, string password);
        void Disconnect();
    }
}