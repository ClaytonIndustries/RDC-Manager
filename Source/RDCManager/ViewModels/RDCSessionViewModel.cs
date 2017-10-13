using Caliburn.Micro;
using RDCManager.Messages;
using RDCManager.Models;
using MaterialDesignThemes.Wpf;

namespace RDCManager.ViewModels
{
    public class RDCSessionViewModel : Screen, IHandle<RDCSelectedMessage>, IHandle<StopRDCMessage>
    {
        private RDC _selectedRDC;
        public RDC SelectedRDC
        {
            get { return _selectedRDC; }
            set
            {
                _selectedRDC = value;
                NotifyOfPropertyChange(() => SelectedRDC);
                NotifyOfPropertyChange(() => NoSelectedRDC);
                NotifyOfPropertyChange(() => StoppedRDC);
            }
        }

        public bool NoSelectedRDC
        {
            get { return SelectedRDC == null; }
        }

        public bool StoppedRDC
        {
            get { return SelectedRDC != null && !SelectedRDC.IsRunning; }
        }

        private readonly IEventAggregator _events;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IRDCInstanceManager _rdcInstanceManager;

        public RDCSessionViewModel(IEventAggregator events, ISnackbarMessageQueue snackbarMessageQueue, IRDCInstanceManager rdcInstanceManager)
        {
            _events = events;
            _events.Subscribe(this);

            _snackbarMessageQueue = snackbarMessageQueue;
            _rdcInstanceManager = rdcInstanceManager;
        }

        public void Start()
        {
            if (SelectedRDC != null && !SelectedRDC.IsRunning)
            {
                SelectedRDC.Connect();
            }
        }

        public void Save()
        {
            string saveMessage = _rdcInstanceManager.Save() ? "Changes saved" : "Failed to save";

            _snackbarMessageQueue.Enqueue(saveMessage);
        }

        public void Delete()
        {
            if (SelectedRDC != null)
            {
                _rdcInstanceManager.Delete(SelectedRDC);
                _rdcInstanceManager.Save();

                SelectedRDC = null;

                _snackbarMessageQueue.Enqueue("RDC deleted");
            }
        }

        public void Handle(RDCSelectedMessage message)
        {
            if (message.SelectedRDC != null)
            {
                SelectedRDC = message.SelectedRDC;
            }
        }

        public void Handle(StopRDCMessage message)
        {
            if (SelectedRDC != null && SelectedRDC.IsRunning)
            {
                SelectedRDC.Disconnect();
            }
        }
    }
}
