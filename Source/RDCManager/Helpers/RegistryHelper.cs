using System.Reflection;
using Microsoft.Win32;

namespace RDCManager.Helpers
{
    public static class RegistryHelper
    {
        public static void AddStartupKey()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (key.GetValue("RDC Manager") == null)
            {
                key.SetValue("RDC Manager", Assembly.GetExecutingAssembly().Location);
            }
        }

        public static void RemoveStartupKey()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (key.GetValue("RDC Manager") != null)
            {
                key.DeleteValue("RDC Manager", false);
            }
        }
    }
}