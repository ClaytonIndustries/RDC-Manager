using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCListViewModel : Screen
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

        private bool _dialogOpen;
        public bool DialogOpen
        {
            get { return _dialogOpen; }
            set { _dialogOpen = value; NotifyOfPropertyChange(() => DialogOpen); }
        }

        public bool RDCSelectedAndNotRunning
        {
            get { return SelectedRDC != null && !SelectedRDC.IsRunning; }
        }

        private readonly IEventAggregator _events;
        private readonly IFileAccess _fileAccess;

        public RDCListViewModel(IEventAggregator events, IFileAccess fileAccess)
        {
            _events = events;
            _events.Subscribe(this);

            _fileAccess = fileAccess;
        }

        protected override void OnActivate()
        {
            LoadRDCs();
        }

        protected override void OnDeactivate(bool close)
        {
            SaveRDCs();
        }

        public void NewRDC()
        {
            RDCs.Add(new RDC());
        }

        public void DeleteRDC()
        {
            if (SelectedRDC != null)
            {
                RDCs.Remove(SelectedRDC);

                if (RDCs.Count > 0)
                {
                    SelectedRDC = RDCs.First();
                }

                SaveRDCs();
            }
        }

        public void SaveRDC()
        {
            SaveRDCs();
        }

        public void StartRDC()
        {
            if (SelectedRDC != null && !SelectedRDC.IsRunning)
            {
                SelectedRDC.IsRunning = true;
                SelectedRDC.Connect();
            }
        }

        public void StopRDC()
        {
            if (SelectedRDC != null && SelectedRDC.IsRunning)
            {
                SelectedRDC.IsRunning = false;
            }
        }

        public void OpenRDCList()
        {
            DialogOpen = true;
        }

        public void CloseRDCList()
        {
            DialogOpen = false;
        }

        private void LoadRDCs()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                RDCs = new ObservableCollection<RDC>(_fileAccess.Read<List<RDCModel>>(saveLocation)
                                                                .Select(x => new RDC()
                                                                {
                                                                    DisplayName = x.DisplayName,
                                                                    MachineName = x.MachineName,
                                                                    Username = x.Username,
                                                                    Password = x.Password
                                                                })
                                                    );

                if (RDCs.Count > 0)
                {
                    SelectedRDC = RDCs.First();
                }
            }
            catch
            {
                RDCs = new ObservableCollection<RDC>();
            }
        }

        private void SaveRDCs()
        {
            try
            {
                string saveLocation = System.AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                List<RDCModel> rdcs = RDCs.Select(x => new RDCModel()
                                                    {
                                                        DisplayName = x.DisplayName,
                                                        MachineName = x.MachineName,
                                                        Username = x.Username,
                                                        Password = x.Password
                                                    }).ToList();

                _fileAccess.Write(saveLocation, rdcs);
            }
            catch
            {
            }
        }
    }
}
