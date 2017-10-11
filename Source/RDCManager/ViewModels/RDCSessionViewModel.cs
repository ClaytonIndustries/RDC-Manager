using Caliburn.Micro;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCSessionViewModel : Screen, IHandle<RDCSelectedMessage>
    {
        private RDC _selectedRDC;
        public RDC SelectedRDC
        {
            get { return _selectedRDC; }
            set { _selectedRDC = value; NotifyOfPropertyChange(() => SelectedRDC); }
        }

        private readonly IEventAggregator _events;
        private readonly IRDCInstanceManager _rdcInstanceManager;

        public RDCSessionViewModel(IEventAggregator events, IRDCInstanceManager rdcInstanceManager)
        {
            _events = events;
            _events.Subscribe(this);

            _rdcInstanceManager = rdcInstanceManager;
        }

        public void Start()
        {
            if (SelectedRDC != null && !SelectedRDC.IsRunning)
            {
                SelectedRDC.Connect();
                // Only set to true if connect succeeds
                SelectedRDC.IsRunning = true;
            }
        }

        public void Stop()
        {
            if (SelectedRDC != null && SelectedRDC.IsRunning)
            {
                SelectedRDC.IsRunning = false;
            }
        }

        public void Save()
        {
            _rdcInstanceManager.Save();
        }

        public void Delete()
        {
        }

        public void Handle(RDCSelectedMessage message)
        {
            if (message.SelectedRDC != null)
            {
                SelectedRDC = message.SelectedRDC;
            }
        }
    }
}
