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
        private static bool isRegistered = false;
        public static IDisposable Register(IntPtr windowHandle, Action callBack)
        {
            if (isRegistered) throw new InvalidProgramException("二度登録されるケースは想定外");//ホットキーの設定変更のユースケースでは、登録解除に対応する必要があるかも

            HotKeyHandle handle = new HotKeyHandle(callBack, windowHandle);
            return handle;
        }
    }
}
