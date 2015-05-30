using Caliburn.Micro;
using RDCManager.ViewModels;
using System.Windows;

namespace RDCManager.Bootstrappers
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _simpleContainer = new SimpleContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            App.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            App.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow.ResizeMode = ResizeMode.CanMinimize;

            App.Current.MainWindow.MinWidth = 300;
        }
    }
}
