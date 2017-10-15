using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;

namespace RDCManager.Models
{
    public class RDCInstanceManager : IRDCInstanceManager
    {
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IFileAccess _fileAccess;

        private List<RDC> _rdcs;

        public RDCInstanceManager(ISnackbarMessageQueue snackbarMessageQueue, IFileAccess fileAccess)
        {
            _snackbarMessageQueue = snackbarMessageQueue;
            _fileAccess = fileAccess;

            Load();
        }

        public IEnumerable<RDC> GetRDCs()
        {
            return _rdcs;
        }

        public RDC CreateNew()
        {
            RDC rdc = new RDC(_snackbarMessageQueue)
            {
                DisplayName = "New RDC",
                MachineName = "new.rdc"
            };

            _rdcs.Add(rdc);

            return rdc;
        }

        public void Delete(RDC rdc)
        {
            _rdcs.Remove(rdc);
        }

        public bool Save()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                List<RDCModel> rdcs = _rdcs.Select(x => new RDCModel()
                {
                    DisplayName = x.DisplayName,
                    MachineName = x.MachineName,
                    Username = x.Username,
                    Password = x.Password,
                    Domain = x.Domain,
                    UserAccountId = x.UserAccountId
                }).ToList();

                _fileAccess.Write(saveLocation, rdcs);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Load()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCConnections.xml";

                _rdcs = _fileAccess.Read<List<RDCModel>>(saveLocation)
                                   .Select(x => new RDC(_snackbarMessageQueue)
                                   {
                                       DisplayName = x.DisplayName,
                                       MachineName = x.MachineName,
                                       Username = x.Username,
                                       Password = x.Password,
                                       Domain = x.Domain,
                                       UserAccountId = x.UserAccountId
                                   })
                                   .ToList();
            }
            catch
            {
                _rdcs = new List<RDC>();
            }
        }
    }
}