using System.Collections.ObjectModel;
using Caliburn.Micro;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCCollectionViewModel : Screen
    {
        private ObservableCollection<RDC> _rdcs;
        public ObservableCollection<RDC> RDCs
        {
            get { return _rdcs; }
            private set { _rdcs = value; NotifyOfPropertyChange(() => RDCs); }
        }

        private RDC _selectedRDC;
        public RDC SelectedRDC
        {
            get { return _selectedRDC; }
            set { _selectedRDC = value; SelectedRDCChanged(); NotifyOfPropertyChange(() => SelectedRDC); }
        }

        private readonly IEventAggregator _events;
        private readonly IRDCInstanceManager _rdcInstanceManager;

        public RDCCollectionViewModel(IEventAggregator events, IRDCInstanceManager rdcInstanceManager)
        {
            _events = events;
            _rdcInstanceManager = rdcInstanceManager;

            RDCs = new ObservableCollection<RDC>(rdcInstanceManager.GetRDCs());
        }

        private void SelectedRDCChanged()
        {
            _events.PublishOnUIThread(new RDCSelectedMessage() { SelectedRDC = SelectedRDC });
        }
    }
}