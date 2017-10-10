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

            axMsTscAxNotSafeForScripting.OnConnecting += (s, e) =>
            {
            };

            axMsTscAxNotSafeForScripting.OnConnected += (s, e) =>
            {
            };

            axMsTscAxNotSafeForScripting.OnAuthenticationWarningDisplayed += (s, e) =>
            {
            };

            axMsTscAxNotSafeForScripting.OnDisconnected += (s, e) =>
            {
                RaiseDisconnectedEvent();
            };
        }

        public void Connect(string machineName, string username, string password)
         {
            if (axMsTscAxNotSafeForScripting.Connected == 0)
            {
               
                axMsTscAxNotSafeForScripting.Server = machineName;
                axMsTscAxNotSafeForScripting.UserName = username;
                axMsTscAxNotSafeForScripting.Domain = "MicrosoftAccount";

                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).EncryptionEnabled = 1;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).AuthenticationLevel = 2;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).EnableCredSspSupport = true;
                //((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).SmartSizing = true;
                ((IMsRdpClientAdvancedSettings8)axMsTscAxNotSafeForScripting.AdvancedSettings).BitmapPersistence = 1;

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
