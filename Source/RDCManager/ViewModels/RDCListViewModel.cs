using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Controls;
using Caliburn.Micro;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCListViewModel : Screen
    {
        private ObservableCollection<RDCSession> _rdcs;
        public ObservableCollection<RDCSession> RDCs
        {
            get { return _rdcs; }
            private set { _rdcs = value; NotifyOfPropertyChange(() => RDCs); }
        }

        private RDCSession _selectedRDC;
        public RDCSession SelectedRDC
        {
            get { return _selectedRDC; }
            set { _selectedRDC = value; SelectedRDCChanged(); NotifyOfPropertyChange(() => SelectedRDC); }
        }

        private Grid _rdcFrame;

        private readonly IEventAggregator _events;
        private readonly IRDCStarter _rdcStarter;
        private readonly IFileAccess _fileAccess;
        private readonly Timer _timer;

        public RDCListViewModel(IEventAggregator events, IRDCStarter rdcStarter, IFileAccess fileAccess)
        {
            _events = events;
            _events.Subscribe(this);

            _rdcStarter = rdcStarter;
            _fileAccess = fileAccess;

            _timer = new Timer(5000);
            _timer.AutoReset = true;
            _timer.Elapsed += delegate
            {
                if (RDCs != null)
                {
                    foreach (RDCSession rdc in RDCs)
                    {
                        rdc.CheckForDeadProcess();
                    }
                }
            };
            _timer.Start();
        }

        protected override void OnActivate()
        {
            LoadRDCs();
        }

        protected override void OnDeactivate(bool close)
        {
            SaveRDCs();
        }

        public void RDCFrameLoaded(Grid grid)
        {
            _rdcFrame = grid;

            _rdcFrame.SizeChanged += delegate
            {
                foreach(RDCSession rdc in _rdcs)
                {
                    rdc.CalculateSessionSize(_rdcFrame);
                }
            };
        }

        public void NewRDC()
        {
            RDCs.Add(new RDCSession());
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
                SelectedRDC.CreateSession(_rdcStarter, _rdcFrame);
            }
        }

        public void StopRDC()
        {
            if (SelectedRDC != null && SelectedRDC.IsRunning)
            {
                SelectedRDC.EndSession();
            }
        }

        private void SelectedRDCChanged()
        {
            if (_selectedRDC != null)
            {
                _selectedRDC.ShowSession();

                foreach (RDCSession rdc in _rdcs)
                {
                    if (rdc != _selectedRDC)
                    {
                        rdc.HideSession();
                    }
                }
            }
        }

        private void LoadRDCs()
        {
            try
            {
                string saveLocation = System.AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                RDCs = new ObservableCollection<RDCSession>(_fileAccess.Read<List<RDCModel>>(saveLocation)
                                                                .Select(x => new RDCSession()
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
                RDCs = new ObservableCollection<RDCSession>();
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
