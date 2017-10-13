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
        }

        public void Connect(string machineName, string username, string password)
         {
            if (axMsTscAxNotSafeForScripting.Connected == 0)
            {
                _disconnectRequested = false;

                axMsTscAxNotSafeForScripting.Server = machineName;
                axMsTscAxNotSafeForScripting.UserName = username;
                axMsTscAxNotSafeForScripting.Domain = "MicrosoftAccount";
                axMsTscAxNotSafeForScripting.ConnectingText = $"Connecting to {machineName}";

                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).EncryptionEnabled = 1;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).AuthenticationLevel = 2;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).EnableCredSspSupport = true;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).SmartSizing = true;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).BitmapPersistence = 1;

                axMsTscAxNotSafeForScripting.DesktopWidth = Screen.PrimaryScreen.Bounds.Width - 50;
                axMsTscAxNotSafeForScripting.DesktopHeight = Screen.PrimaryScreen.Bounds.Height - 63;

                IMsTscNonScriptable secured = (IMsTscNonScriptable)axMsTscAxNotSafeForScripting.GetOcx();
                secured.ClearTextPassword = password;

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
