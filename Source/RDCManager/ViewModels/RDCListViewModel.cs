using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows;
using Caliburn.Micro;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCListViewModel : Screen, IHandle<NewRDCConnectionMessage>
    {
        public ObservableCollection<RDCConnection> RDCConnections
        {
            get;
            private set;
        }

        public RDCConnection SelectedRDCConnection
        {
            get;
            set;
        }

        private readonly IEventAggregator _events;

        private readonly IRDCStarter _rdcStarter;

        public RDCListViewModel(IEventAggregator events, IRDCStarter rdcStarter)
        {
            _events = events;
            _events.Subscribe(this);

            _rdcStarter = rdcStarter;

            RDCConnections = new ObservableCollection<RDCConnection>();
        }

        public void New()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.MinWidth = 250;
            settings.Title = "New RDC Connection";

            new WindowManager().ShowDialog(new NewRDCConnectionViewModel(_events), null, settings);
        }

        public void Delete()
        {
            if(SelectedRDCConnection != null)
            {
                RDCConnections.Remove(SelectedRDCConnection);
            }
        }

        public void Start()
        {
            if(SelectedRDCConnection != null)
            {
                _rdcStarter.StartRDCSession(SelectedRDCConnection.MachineName);
            }
        }

        public void Handle(NewRDCConnectionMessage message)
        {
            RDCConnections.Add(new RDCConnection(message.DisplayName, message.MachineName));
        }
    }
}
