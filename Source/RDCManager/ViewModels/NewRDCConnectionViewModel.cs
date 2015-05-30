using Caliburn.Micro;
using RDCManager.Messages;

namespace RDCManager.ViewModels
{
    public class NewRDCConnectionViewModel : Screen
    {
        public new string DisplayName
        {
            get;
            set;
        }

        public string MachineName
        {
            get;
            set;
        }

        private readonly IEventAggregator _events;

        public NewRDCConnectionViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public void Add()
        {
            if(!string.IsNullOrWhiteSpace(DisplayName) && !string.IsNullOrWhiteSpace(MachineName))
            {
                _events.PublishOnUIThread(new NewRDCConnectionMessage(DisplayName, MachineName));

                Cancel();
            }
        }

        public void Cancel()
        {
            TryClose();
        }
    }
}