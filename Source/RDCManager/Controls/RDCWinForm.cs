using System;
using System.Windows.Forms;
using MSTSCLib;

namespace RDCManager.Controls
{
    public partial class RDCWinForm : UserControl
    {
        public event EventHandler Disconnected;

        public RDCWinForm()
        {
            InitializeComponent();

            axMsTscAxNotSafeForScripting.OnDisconnected += (s,e) => RaiseDisconnectedEvent();
        }

        public void Connect(string machineName, string username, string password)
        {
            if (axMsTscAxNotSafeForScripting.Connected == 0)
            {
                axMsTscAxNotSafeForScripting.Server = machineName;
                axMsTscAxNotSafeForScripting.UserName = username;

                axMsTscAxNotSafeForScripting.DesktopWidth = Screen.PrimaryScreen.Bounds.Width;
                axMsTscAxNotSafeForScripting.DesktopHeight = Screen.PrimaryScreen.Bounds.Height;

                IMsTscNonScriptable secured = (IMsTscNonScriptable)axMsTscAxNotSafeForScripting.GetOcx();
                secured.ClearTextPassword = password;

                axMsTscAxNotSafeForScripting.Connect();
            }
        }

        public void Disconnect()
        {
            if (axMsTscAxNotSafeForScripting.Connected == 1)
            {
                axMsTscAxNotSafeForScripting.Disconnect();
            }
        }

        private void RaiseDisconnectedEvent()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }
    }
}
