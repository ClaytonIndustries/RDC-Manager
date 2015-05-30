namespace RDCManager.Models
{
    public class RDCConnection
    {
        public string DisplayName
        {
            get;
            private set;
        }

        public string MachineName
        {
            get;
            private set;
        }

        public RDCConnection(string displayName, string machineName)
        {
            DisplayName = displayName;
            MachineName = machineName;
        }
    }
}