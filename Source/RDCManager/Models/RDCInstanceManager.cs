using System;
using System.Collections.Generic;
using System.Linq;

namespace RDCManager.Models
{
    public class RDCInstanceManager : IRDCInstanceManager
    {
        private readonly IFileAccess _fileAccess;

        private ICollection<RDC> _rdcs;

        public RDCInstanceManager(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;

            Load();
        }

        public IEnumerable<RDC> GetRDCs()
        {
            return _rdcs;
        }

        public RDC CreateNew()
        {
            RDC rdc = new RDC()
            {
                DisplayName = "New RDC",
                MachineName = "new.rdc"
            };

            _rdcs.Add(rdc);

            return rdc;
        }

        public void Save()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                List<RDCModel> rdcs = _rdcs.Select(x => new RDCModel()
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

        private void Load()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                _rdcs = new List<RDC>(_fileAccess.Read<List<RDCModel>>(saveLocation)
                                                  .Select(x => new RDC()
                                                  {
                                                      DisplayName = x.DisplayName,
                                                      MachineName = x.MachineName,
                                                      Username = x.Username,
                                                      Password = x.Password
                                                  })
                                     );
            }
            catch
            {
                _rdcs = new List<RDC>();
            }
        }
    }
}