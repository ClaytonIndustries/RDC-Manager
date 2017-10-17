using System.Collections.ObjectModel;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
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

        public RDCGroupsViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _snackbarMessageQueue = snackbarMessageQueue;
        }

        public void New()
        {
        }

        public void Save()
        {
        }

        public void Delete()
        {
        }
    }
}