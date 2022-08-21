using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace LauncherModelLib.HotKey
{
    public static class GlobalHotKeyFactory
    {
        public static IDisposable Register(IntPtr windowHandle, Action callBack)
        {
            //TODO:二回呼ばれないようにする
            //TODO:定義を整理する
            HotKeyHandle handle = new HotKeyHandle(callBack, windowHandle);
            return handle;
        }
    }
}
