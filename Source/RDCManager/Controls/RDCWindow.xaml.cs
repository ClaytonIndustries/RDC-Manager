using System;
using System.Windows.Controls;
using RDCManager.Models;

namespace RDCManager.Controls
{
    /// <summary>
    /// Interaction logic for RDCWindow.xaml
    /// </summary>
    public partial class RDCWindow : UserControl, IRDCWindow
    {
        public event EventHandler Disconnected;

        public RDCWindow()
        {
            InitializeComponent();

            RDCWinForm.Disconnected += (s,e) => Disconnected?.Invoke(this, EventArgs.Empty);
        }

        public void Connect(string machineName, string displayName, string username, string password, string domain)
        {
            RDCWinForm.Connect(machineName, displayName, username, password, domain);
        }

        public void Disconnect()
        {
            RDCWinForm.Disconnect();
        }
    }
}