using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using RDCManager.Converters;
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
            set { _selectedRDC = value; NotifyOfPropertyChange(() => SelectedRDC); }
        }

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
            set { _selectedRDCGroup = value; _groupView?.Refresh(); NotifyOfPropertyChange(() => SelectedRDCGroup); }
        }

        private readonly IEventAggregator _events;
        private readonly IRDCInstanceManager _rdcInstanceManager;
        private readonly IRDCGroupManager _rdcGroupManager;

        private ICollectionView _groupView;

        public RDCCollectionViewModel(IEventAggregator events, IRDCInstanceManager rdcInstanceManager, IRDCGroupManager rdcGroupManager)
        {
            _events = events;
            _rdcInstanceManager = rdcInstanceManager;
            _rdcGroupManager = rdcGroupManager;
        }

        protected override void OnActivate()
        {
            RDCs = new ObservableCollection<RDC>(_rdcInstanceManager.GetRDCs());
            RDCGroups = new ObservableCollection<RDCGroup>(_rdcGroupManager.GetGroups());

            RDCGroups.Insert(0, new RDCGroup() { Name = "All", Id = Guid.Empty });

            _groupView = CollectionViewSource.GetDefaultView(_rdcs);
            _groupView.GroupDescriptions.Add(new PropertyGroupDescription("GroupId", new GroupIdConverter(_rdcGroups)));
            _groupView.Filter += (item) =>
            {
                RDC rdc = item as RDC;

                if (SelectedRDCGroup == null || SelectedRDCGroup.Id == Guid.Empty || rdc.GroupId == SelectedRDCGroup.Id)
                {
                    return true;
                }

                return false;
            };

            SelectedRDCGroup = RDCGroups.First();
        }

        public void RDCSelected()
        {
            foreach(RDC rdc in RDCs)
            {
                rdc.IsSelected = rdc == SelectedRDC;
            }

            _events.PublishOnUIThread(new RDCSelectedMessage() { SelectedRDC = SelectedRDC });
        }

        public void NewRDC()
        {
            RDC rdc = _rdcInstanceManager.CreateNew();

            RDCs.Add(rdc);

            SelectedRDC = rdc;

            RDCSelected();
        }
    }
}