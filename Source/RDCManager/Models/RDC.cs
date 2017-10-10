using Caliburn.Micro;
using RDCManager.Controls;

namespace RDCManager.Models
{
    public class RDC : PropertyChangedBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; NotifyOfPropertyChange(() => DisplayName); }
        }

        private string _machineName;
        public string MachineName
        {
            get { return _machineName; }
            set { _machineName = value; NotifyOfPropertyChange(() => MachineName); }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyOfPropertyChange(() => Username); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyOfPropertyChange(() => Password); }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; NotifyOfPropertyChange(() => IsRunning); }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; NotifyOfPropertyChange(() => IsRunning); }
        }

        private RDCWindow _session;
        public RDCWindow Session
        {
            get { return _session; }
            set { _session = value; NotifyOfPropertyChange(() => Session); }
        }

        public RDC()
        {
            Session = new RDCWindow();

            Session.Disconnected += delegate
            {
                IsRunning = false;
            };
        }

        public void Connect()
        {
            // Validate arguments / add try catch / return success or failure
            //Session.Connect(MachineName, Username, Password);
            Session.Connect("104.40.157.151", "mcl32", "Bubble16Bubble16!");
        }

        public void Disconnect()
        {
            // Add try catch
            Session.Disconnect();
        }
    }
}