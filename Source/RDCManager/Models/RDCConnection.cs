namespace RDCManager.Models
{
    public class RDCConnection
    {
        public string DisplayName
        {
            get;
            set;
        }

        public string MachineName
        {
            get;
            set;
        }

        public RDCConnection(string displayName, string machineName)
        {
            DisplayName = displayName;
            MachineName = machineName;
        }

        public RDCConnection()
        {
        }
    }
}