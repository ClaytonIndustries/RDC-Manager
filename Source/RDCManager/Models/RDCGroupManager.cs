using System;
using System.Collections.Generic;
using System.Linq;

namespace RDCManager.Models
{
    public class RDCGroupManager : IRDCGroupManager
    {
        private readonly IFileAccess _fileAccess;

        private ICollection<RDCGroup> _rdcGroups;

        public RDCGroupManager(IFileAccess fileAccess)
        {
            _fileAccess = fileAccess;

            Load();
        }

        public IEnumerable<RDCGroup> GetGroups()
        {
            return _rdcGroups;
        }

        public RDCGroup CreateNew()
        {
            RDCGroup rdcGroup = new RDCGroup()
            {
                Id = Guid.NewGuid()
            };

            _rdcGroups.Add(rdcGroup);

            return rdcGroup;
        }

        public void Delete(RDCGroup rdcGroup)
        {
            _rdcGroups.Remove(rdcGroup);
        }

        public bool Save()
        {
            try
            {
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCGroups.json";

                _fileAccess.Write(saveLocation, _rdcGroups);

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
                string saveLocation = AppDomain.CurrentDomain.BaseDirectory + "RDCGroups.json";

                _rdcGroups = _fileAccess.Read<IEnumerable<RDCGroup>>(saveLocation).ToList();
            }
            catch
            {
                _rdcGroups = new List<RDCGroup>();
            }
        }
    }
}