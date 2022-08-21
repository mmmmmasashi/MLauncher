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
        //グローバルホットキーの実装の参考
        //全体像 : https://social.technet.microsoft.com/wiki/contents/articles/30568.wpf-implementing-global-hot-keys.aspx
        //RegisterHotKey https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-registerhotkey?redirectedfrom=MSDN
        //Virtual-Key https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS

        private const uint VK_Z = 0x5A;
        private const uint VK_CAPITAL = 0x14;
        private const uint VK_HOME = 0x24;

        private IntPtr _windowHandle;
        private HwndSource _source;


        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            var isSuccess = RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT, VK_Z); //Ctrl + Shift + Z
            Trace.WriteLine($"isSuccess:{isSuccess}");
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            if (msg != WM_HOTKEY) return IntPtr.Zero;
            if (wParam.ToInt32() != HOTKEY_ID) return IntPtr.Zero;

            int modifier = (int)lParam & 0xFFFF;
            int key = (((int)lParam >> 16) & 0xFFFF);

            if (modifier == (MOD_CONTROL | MOD_SHIFT) && key == VK_Z)
            {
                Trace.WriteLine("Called");
            }

            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
            base.OnClosed(e);
        }
    }
}
