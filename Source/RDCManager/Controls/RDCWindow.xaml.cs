using System;
using System.Windows.Controls;

namespace RDCManager.Controls
{
    /// <summary>
    /// Interaction logic for RDCWindow.xaml
    /// </summary>
    public partial class RDCWindow : UserControl
    {
        public event EventHandler Disconnected;

        private readonly RDCWinForm _rdcControl;

        public RDCWindow()
        {
            InitializeComponent();

            _rdcControl = new RDCWinForm();

            _rdcControl.Disconnected += (s,e) => Disconnected?.Invoke(this, EventArgs.Empty);

            RDCHost.Child = _rdcControl;
        }

        public void Connect(string machineName, string username, string password)
        {
            _rdcControl.Connect(machineName, username, password);
        }

        public void Disconnect()
        {
            _rdcControl.Disconnect();
        }


        /*
         * 
         * Turn this + RDC into a view / view model so that it can be bound to the content control and list view
         * 
         */


        //public string MachineName
        //{
        //    get { return (string)GetValue(MachineNameProperty); }
        //    set { SetValue(MachineNameProperty, value); }
        //}

        //public static readonly DependencyProperty MachineNameProperty = 
        //    DependencyProperty.Register(
        //    "MachineName", typeof(string), typeof(RDCWindow),
        //    new PropertyMetadata(default(string), null));

        //public string Username
        //{
        //    get { return (string)GetValue(UsernameProperty); }
        //    set { SetValue(UsernameProperty, value); }
        //}

        //public static readonly DependencyProperty UsernameProperty =
        //    DependencyProperty.Register(
        //    "Username", typeof(string), typeof(RDCWindow),
        //    new PropertyMetadata(default(string), null));

        //public string Password
        //{
        //    get { return (string)GetValue(PasswordProperty); }
        //    set { SetValue(PasswordProperty, value); }
        //}

        //public static readonly DependencyProperty PasswordProperty =
        //    DependencyProperty.Register(
        //    "Password", typeof(string), typeof(RDCWindow),
        //    new PropertyMetadata(default(string), null));

        //public bool IsRunning
        //{
        //    get { return (bool)GetValue(IsRunningProperty); }
        //    set { SetValue(IsRunningProperty, value); }
        //}

        //public static readonly DependencyProperty IsRunningProperty =
        //    DependencyProperty.Register(
        //    "IsRunning", typeof(bool), typeof(RDCWindow),
        //    new PropertyMetadata(default(bool), IsRunningPropertyChanged));

        //private static void IsRunningPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var rdcWindow = d as RDCWindow;

        //    if(rdcWindow.IsRunning)
        //    {
        //        rdcWindow._rdc.Connect(rdcWindow.MachineName, rdcWindow.Username, rdcWindow.Password);
        //    }
        //    else
        //    {
        //        rdcWindow._rdc.Disconnect();
        //    }
        //}

        //public bool IsVisible
        //{
        //    get { return (bool)GetValue(IsVisibleProperty); }
        //    set { SetValue(IsVisibleProperty, value); }
        //}

        //public static readonly DependencyProperty IsVisibleProperty =
        //    DependencyProperty.Register(
        //    "IsVisible", typeof(bool), typeof(RDCWindow),
        //    new PropertyMetadata(default(bool), IsVisiblePropertyChanged));

        //private static void IsVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var rdcWindow = d as RDCWindow;

        //    if (rdcWindow.IsVisible)
        //    {
        //        rdcWindow.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        rdcWindow.Visibility = Visibility.Hidden;
        //    }
        //}
    }
}