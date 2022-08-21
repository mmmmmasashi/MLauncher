using LauncherModelLib.HotKey;
using MahApps.Metro.Controls;

//for hotkey
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace MLauncherApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IDisposable _hotKeyHandle;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            _hotKeyHandle = GlobalHotKeyFactory.Register(windowHandle, CallBackMethod);
        }

        private void CallBackMethod()
        {
            Trace.WriteLine("CALLED!");
        }

        protected override void OnClosed(EventArgs e)
        {
            _hotKeyHandle.Dispose();
            base.OnClosed(e);
        }
    }
}
