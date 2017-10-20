using System;
using System.Windows.Forms;
using MSTSCLib;

namespace RDCManager.Controls
{
    public partial class RDCWinForm : UserControl
    {
        public event EventHandler Disconnected;

        private bool _disconnectRequested;

        public RDCWinForm()
        {
            InitializeComponent();

            axMsTscAxNotSafeForScripting.OnDisconnected += (s, e) =>
            {
                Disconnected?.Invoke(this, EventArgs.Empty);
            };

            axMsTscAxNotSafeForScripting.OnConnected += (s, e) =>
            {
                DisplayName.BringToFront();
            };
        }

        public void Connect(string machineName, string displayName, string username, string password, string domain)
        {
            if (axMsTscAxNotSafeForScripting.Connected == 0)
            {
                _disconnectRequested = false;

                axMsTscAxNotSafeForScripting.Server = machineName;
                axMsTscAxNotSafeForScripting.UserName = username;
                axMsTscAxNotSafeForScripting.Domain = domain;
                axMsTscAxNotSafeForScripting.ConnectingText = $"Connecting to {machineName}";
                axMsTscAxNotSafeForScripting.DesktopWidth = Screen.PrimaryScreen.Bounds.Width - 50;
                axMsTscAxNotSafeForScripting.DesktopHeight = Screen.PrimaryScreen.Bounds.Height - 63;

                IMsRdpClientAdvancedSettings7 advancedSettings = (IMsRdpClientAdvancedSettings7)axMsTscAxNotSafeForScripting.AdvancedSettings;
                advancedSettings.EncryptionEnabled = 1;
                advancedSettings.AuthenticationLevel = 2;
                advancedSettings.EnableCredSspSupport = true;
                advancedSettings.SmartSizing = true;
                advancedSettings.BitmapPersistence = 1;

                IMsTscNonScriptable secureSettings = (IMsTscNonScriptable)axMsTscAxNotSafeForScripting.GetOcx();
                secureSettings.ClearTextPassword = password;

                DisplayName.Text = displayName;

                axMsTscAxNotSafeForScripting.Connect();
            }
        }

        public void Disconnect()
        {
            if (axMsTscAxNotSafeForScripting.Connected == 1 && !_disconnectRequested)
            {
                _disconnectRequested = true;

                axMsTscAxNotSafeForScripting.Disconnect();
            }
        }
    }
}
