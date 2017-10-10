using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using RDCManager.Models;
using RDCManager.ViewModels;

namespace RDCManager.Bootstrappers
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        private NotifyIcon notifyIcon;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            ConfigureMainWindow();

            CreateTrayIcon();

            Application.MainWindow.StateChanged += MainWindow_StateChanged;
            Application.MainWindow.Closing += MainWindow_Closing;
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container.PerRequest<IFileAccess, XmlFileAccess>();
            _container.PerRequest<IWindowManager, WindowManager>();

            _container.RegisterInstance(typeof(IEventAggregator), null, new EventAggregator());

            _container.RegisterSingleton(typeof(ShellViewModel), null, typeof(ShellViewModel));
            _container.RegisterSingleton(typeof(RDCListViewModel), null, typeof(RDCListViewModel));
        }

        private void ConfigureMainWindow()
        {
            App.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            App.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            App.Current.MainWindow.WindowState = WindowState.Maximized;
            App.Current.MainWindow.MinWidth = 700;
            App.Current.MainWindow.MinHeight = 500;
            App.Current.MainWindow.Icon = new BitmapImage(new Uri("pack://application:,,,/RDCManager;component/Assets/WindowIcon.png"));

            string[] args = Environment.GetCommandLineArgs();

            if (ShouldStartMinimised())
            {
                App.Current.MainWindow.WindowState = WindowState.Minimized;
                App.Current.MainWindow.Hide();
            }
        }

        private bool ShouldStartMinimised()
        {
            return Environment.GetCommandLineArgs().ToList().Contains("-m");
        }

        private void CreateTrayIcon()
        {
            notifyIcon = new NotifyIcon();

            notifyIcon.Icon = new Icon(GetTrayIconPath());
            notifyIcon.Visible = true;
            notifyIcon.Text = "RDC Manager";

            MenuItem[] menuItems = new MenuItem[2]
            {
                new MenuItem( "Open", TrayIconOpenClicked ),
                new MenuItem( "Close", TrayIconCloseClicked )
            };

            notifyIcon.ContextMenu = new ContextMenu(menuItems);

            notifyIcon.DoubleClick += delegate
            {
                Application.MainWindow.Show();
                Application.MainWindow.WindowState = WindowState.Normal;
                Application.MainWindow.Focus();
            };
        }

        private string GetTrayIconPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets\\TrayIcon.ico");
        }

        private void TrayIconOpenClicked(object sender, EventArgs e)
        {
            Application.MainWindow.Show();
            Application.MainWindow.WindowState = WindowState.Normal;
            Application.MainWindow.Focus();
        }

        private void TrayIconCloseClicked(object sender, EventArgs e)
        {
            Application.MainWindow.Close();
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (Application.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.MainWindow.Hide();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            notifyIcon.Visible = false;
        }
    }
}
