using System;

namespace RDCManager.Models
{
    public class RDCModel
    {
        public string DisplayName { get; set; }
        public string MachineName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public Guid UserAccountId { get; set; }
    }
}