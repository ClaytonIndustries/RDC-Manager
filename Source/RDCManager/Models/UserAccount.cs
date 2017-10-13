using System;

namespace RDCManager.Models
{
    public class UserAccount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}