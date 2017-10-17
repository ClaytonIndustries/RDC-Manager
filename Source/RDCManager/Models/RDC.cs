using System;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
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

        private string _domain;
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; NotifyOfPropertyChange(() => Domain); }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; NotifyOfPropertyChange(() => IsRunning); }
        }

        private IRDCWindow _session;
        public IRDCWindow Session
        {
            get { return _session; }
            set { _session = value; NotifyOfPropertyChange(() => Session); }
        }

        public Guid UserAccountId
        {
            get; set;
        }

        public Guid GroupId
        {
            get; set;
        }

        public RDC(ISnackbarMessageQueue snackbarMessageQueue)
        {
            Session = new RDCWindow();

            Session.Disconnected += delegate
            {
                IsRunning = false;

                snackbarMessageQueue.Enqueue($"{DisplayName} disconnected");
            };
        }

        public void Connect()
        {
            try
            {
                IsRunning = true;

                Session.Connect(MachineName, Username, Password, Domain);
            }
            catch
            {
                IsRunning = false;
            }
        }

        public void Disconnect()
        {
            Session.Disconnect();
        }
    }
}