using System.Windows;

namespace RDCManager.Models
{
    public class ApplicationWrapper : IApplicationWrapper
    {
        private readonly Application _application;

        public ApplicationWrapper(Application application)
        {
            _application = application;
        }

        public void ToggleFullScreen()
        {
            _application.MainWindow.WindowState = WindowState.Maximized;

            _application.MainWindow.WindowStyle = _application.MainWindow.WindowStyle == WindowStyle.SingleBorderWindow ? WindowStyle.None : WindowStyle.SingleBorderWindow;
        }
    }
}