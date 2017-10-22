using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Models;
using RDCManager.ViewModels;

namespace RDCManager.Bootstrappers
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            ConfigureMainWindow();
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
            _container.PerRequest<IFileAccess, JsonFileAccess>();
            _container.PerRequest<IEncryptionManager, EncryptionManager>();
            _container.PerRequest<IWindowManager, WindowManager>();

            _container.RegisterInstance(typeof(Application), null, App.Current);
            _container.RegisterInstance(typeof(IEventAggregator), null, new EventAggregator());
            _container.RegisterInstance(typeof(ISnackbarMessageQueue), null, new SnackbarMessageQueue());
            _container.RegisterInstance(typeof(IRDCInstanceManager), null, new RDCInstanceManager(_container.GetInstance<ISnackbarMessageQueue>(), _container.GetInstance<IFileAccess>(), _container.GetInstance<IEncryptionManager>()));
            _container.RegisterInstance(typeof(IUserAccountManager), null, new UserAccountManager(_container.GetInstance<IFileAccess>(), _container.GetInstance<IEncryptionManager>()));
            _container.RegisterInstance(typeof(IRDCGroupManager), null, new RDCGroupManager(_container.GetInstance<IFileAccess>()));
            _container.RegisterInstance(typeof(IApplicationWrapper), null, new ApplicationWrapper(_container.GetInstance<Application>()));

            _container.RegisterSingleton(typeof(ShellViewModel), null, typeof(ShellViewModel));
            _container.RegisterSingleton(typeof(RDCSessionViewModel), null, typeof(RDCSessionViewModel));
            _container.RegisterSingleton(typeof(RDCCollectionViewModel), null, typeof(RDCCollectionViewModel));
            _container.RegisterSingleton(typeof(RDCUserAccountsViewModel), null, typeof(RDCUserAccountsViewModel));
            _container.RegisterSingleton(typeof(RDCGroupsViewModel), null, typeof(RDCGroupsViewModel));
        }

        private void ConfigureMainWindow()
        {
            App.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            App.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            App.Current.MainWindow.WindowState = WindowState.Maximized;
            App.Current.MainWindow.MinWidth = 950;
            App.Current.MainWindow.MinHeight = 650;
            App.Current.MainWindow.Icon = new BitmapImage(new Uri("pack://application:,,,/RDCManager;component/Assets/WindowIcon.png"));
        }
    }
}
