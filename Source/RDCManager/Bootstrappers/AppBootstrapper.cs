using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Caliburn.Micro;
using RDCManager.ViewModels;
using System.Windows.Media.Imaging;
using System;

namespace RDCManager.Bootstrappers
{
    public class AppBootstrapper : BootstrapperBase
    {
        private NotifyIcon notifyIcon;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            ConfigureMainWindow();

            CreateTrayIcon();

            Application.MainWindow.StateChanged += MainWindow_StateChanged;
            Application.MainWindow.Closing += MainWindow_Closing;
        }

        private static void ConfigureMainWindow()
        {
            App.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            App.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow.ResizeMode = ResizeMode.CanMinimize;
            App.Current.MainWindow.MinWidth = 300;
            App.Current.MainWindow.Icon = new BitmapImage(new Uri("pack://application:,,,/RDCManager;component/Assets/WindowIcon.png"));
        }

        private void CreateTrayIcon()
        {
            notifyIcon = new NotifyIcon();

            notifyIcon.Icon = new Icon("Assets\\TrayIcon.ico");
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
                Application.MainWindow.WindowState = System.Windows.WindowState.Normal;
                Application.MainWindow.Focus();
            };
        }

        private void TrayIconOpenClicked(object sender, System.EventArgs e)
        {
            Application.MainWindow.Show();
            Application.MainWindow.WindowState = System.Windows.WindowState.Normal;
            Application.MainWindow.Focus();
        }

        private void TrayIconCloseClicked(object sender, System.EventArgs e)
        {
            Application.MainWindow.Close();
        }

        private void MainWindow_StateChanged(object sender, System.EventArgs e)
        {
            if (Application.MainWindow.WindowState == System.Windows.WindowState.Minimized)
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
