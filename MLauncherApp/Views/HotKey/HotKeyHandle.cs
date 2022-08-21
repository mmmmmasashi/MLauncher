using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace LauncherModelLib.HotKey
{
    internal class HotKeyHandle : IDisposable
    {
        //グローバルホットキーの実装の参考
        //全体像 : https://social.technet.microsoft.com/wiki/contents/articles/30568.wpf-implementing-global-hot-keys.aspx
        //RegisterHotKey https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-registerhotkey?redirectedfrom=MSDN
        //Virtual-Key https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Action _callBack;
        private IntPtr _windowHandle;
        private HwndSource _source;

        const int  HOTKEY_ID = 9000;
        const uint MOD_CONTROL = 0x0002; //CTRL
        const uint MOD_SHIFT = 0x0004; //SHIFT
        const uint VK_Z = 0x5A;

        internal HotKeyHandle(Action callBack, IntPtr windowHandle)
        {
            this._callBack = callBack;
            this._windowHandle = windowHandle;

            HwndSource source = HwndSource.FromHwnd(windowHandle);

            source.AddHook(Hook);
            _source = source;

            var isSuccess = RegisterHotKey(windowHandle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT, VK_Z); //Ctrl + Shift + Z
            if (!isSuccess)
            {
                throw new Exception("ホットキーの登録に失敗しました");
            }
        }

        internal IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            if (msg != WM_HOTKEY) return IntPtr.Zero;
            if (wParam.ToInt32() != HOTKEY_ID) return IntPtr.Zero;

            int modifier = (int)lParam & 0xFFFF;
            int key = (((int)lParam >> 16) & 0xFFFF);

            if (modifier == (MOD_CONTROL | MOD_SHIFT) && key == VK_Z)
            {
                _callBack();
            }

            return IntPtr.Zero;
        }

        public void Dispose()
        {
            _source.RemoveHook(Hook);
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
        }
    }
}
