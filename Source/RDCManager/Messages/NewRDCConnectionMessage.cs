namespace RDCManager.Messages
{
    public class NewRDCConnectionMessage
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

        public NewRDCConnectionMessage(string displayName, string machineName)
        {
            DisplayName = displayName;
            MachineName = machineName;
        }
    }
}
