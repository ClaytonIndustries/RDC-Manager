using System.Collections.ObjectModel;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Controls;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCGroupsViewModel : Screen
    {
        private ObservableCollection<RDCGroup> _rdcGroups;
        public ObservableCollection<RDCGroup> RDCGroups
        {
            get { return _rdcGroups; }
            private set { _rdcGroups = value; NotifyOfPropertyChange(() => RDCGroups); }
        }

        private RDCGroup _selectedRDCGroup;
        public RDCGroup SelectedRDCGroup
        {
            get { return _selectedRDCGroup; }
            set { _selectedRDCGroup = value; NotifyOfPropertyChange(() => SelectedRDCGroup); }
        }

        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IRDCGroupManager _rdcGroupManager;

        public RDCGroupsViewModel(ISnackbarMessageQueue snackbarMessageQueue, IRDCGroupManager rdcGroupManager)
        {
            _snackbarMessageQueue = snackbarMessageQueue;
            _rdcGroupManager = rdcGroupManager;

            RDCGroups = new ObservableCollection<RDCGroup>(_rdcGroupManager.GetGroups());
        }

        public void New()
        {
            RDCGroups.Add(_rdcGroupManager.CreateNew());
        }

        public void Save()
        {
            string saveMessage = _rdcGroupManager.Save() ? "Changes saved" : "Failed to save";

            _snackbarMessageQueue.Enqueue(saveMessage);
        }

        public async void Delete()
        {
            if (SelectedRDCGroup != null)
            {
                object result = await DialogHost.Show(new Dialog());

                if ((bool)result)
                {
                    _rdcGroupManager.Delete(SelectedRDCGroup);
                    _rdcGroupManager.Save();

                    RDCGroups.Remove(SelectedRDCGroup);

                    _snackbarMessageQueue.Enqueue("RDC Group deleted and changes saved");
                }
            }
        }
    }
}