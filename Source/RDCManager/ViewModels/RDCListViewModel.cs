using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
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

        private RDCConnection _selectedRDCConnection;

        public RDCConnection SelectedRDCConnection
        {
            get { return _selectedRDCConnection; }
            set { _selectedRDCConnection = value; NotifyOfPropertyChange(() => SelectedRDCConnection); }
        }

        private readonly IEventAggregator _events;

        private readonly IRDCStarter _rdcStarter;

        private readonly IFileAccess _fileAccess;

        public RDCListViewModel(IEventAggregator events, IRDCStarter rdcStarter, IFileAccess fileAccess)
        {
            _events = events;
            _events.Subscribe(this);

            _rdcStarter = rdcStarter;

            _fileAccess = fileAccess;
        }

        protected override void OnActivate()
        {
            try
            {
                RDCConnections = _fileAccess.Read<ObservableCollection<RDCConnection>>("RDCConnections.xml");

                if (RDCConnections.Count > 0)
                {
                    SelectedRDCConnection = RDCConnections.First();
                }
            }
            catch
            {
                RDCConnections = new ObservableCollection<RDCConnection>();
            }
        }

        protected override void OnDeactivate(bool close)
        {
            try
            {
                _fileAccess.Write("RDCConnections.xml", RDCConnections);
            }
            catch(System.Exception e)
            {
            }
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

            if (RDCConnections.Count == 1)
            {
                SelectedRDCConnection = RDCConnections.First();
            }
        }
    }
}
