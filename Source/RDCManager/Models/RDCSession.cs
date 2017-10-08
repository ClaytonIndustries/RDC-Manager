using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Caliburn.Micro;

namespace RDCManager.Models
{
    public class RDCSession : PropertyChangedBase
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_MAXIMIZEBOX = 0x00010000;
        private const int WS_MINIMIZEBOX = 0x00020000;
        private const int WS_SYSMENU = 0x00080000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; NotifyOfPropertyChange(() => DisplayName); }
        }

        private string _machineName;
        public string MachineName
        {
            get { return _machineName; }
            set { _machineName = value; NotifyOfPropertyChange(() => MachineName); }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyOfPropertyChange(() => Username); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyOfPropertyChange(() => Password); }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; NotifyOfPropertyChange(() => IsRunning); }
        }

        private Process _dockedProcess;
        private IntPtr _hWndDockedWindow;

        public void CreateSession(IRDCStarter rdcStarter, Grid container)
        {
            if (!IsRunning)
            {
                IsRunning = true;

                _dockedProcess = rdcStarter.StartRDCSession(MachineName);

                while (_hWndDockedWindow == IntPtr.Zero)
                {
                    _dockedProcess.WaitForInputIdle(1000);
                    _dockedProcess.Refresh();
                    if (_dockedProcess.HasExited)
                    {
                        IsRunning = false;
                        return;
                    }
                    _hWndDockedWindow = _dockedProcess.MainWindowHandle;
                }

                HwndSource hwndSource = PresentationSource.FromVisual(container) as HwndSource;

                SetParent(_hWndDockedWindow, hwndSource.Handle);

                int dockedWindowStyle = GetWindowLong(_dockedProcess.MainWindowHandle, GWL_STYLE);

                dockedWindowStyle = dockedWindowStyle & ~WS_CAPTION;
                dockedWindowStyle = dockedWindowStyle & ~WS_MAXIMIZEBOX;
                dockedWindowStyle = dockedWindowStyle & ~WS_MINIMIZEBOX;
                dockedWindowStyle = dockedWindowStyle & ~WS_SYSMENU;
                dockedWindowStyle = dockedWindowStyle & ~WS_THICKFRAME;

                SetWindowLong(_dockedProcess.MainWindowHandle, GWL_STYLE, dockedWindowStyle);

                CalculateSessionSize(container);
            }
        }

        public void EndSession()
        {
            if (IsRunning)
            {
                _dockedProcess.Kill();

                _hWndDockedWindow = IntPtr.Zero;

                IsRunning = false;
            }
        }

        public void CalculateSessionSize(Grid container)
        {
            if (IsRunning)
            {
                Point pos = container.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));

                MoveWindow(_hWndDockedWindow, (int)pos.X, (int)pos.Y, (int)container.ActualWidth, (int)container.ActualHeight, true);
            }
        }

        public void ShowSession()
        {
            if (IsRunning)
            {
                ShowWindow(_hWndDockedWindow, SW_SHOW);
            }
        }

        public void HideSession()
        {
            if (IsRunning)
            {
                ShowWindow(_hWndDockedWindow, SW_HIDE);
            }
        }

        public void CheckForDeadProcess()
        {
            if (IsRunning && _dockedProcess != null && _dockedProcess.HasExited)
            {
                _hWndDockedWindow = IntPtr.Zero;

                IsRunning = false;
            }
        }
    }
}