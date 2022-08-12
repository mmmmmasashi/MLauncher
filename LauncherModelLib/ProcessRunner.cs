using MLauncherIF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class ProcessRunner
    {
        public static void Run(FilePath matchedPath)
        {
            if (matchedPath == null) return;

            Process process = new Process();
            process.StartInfo.FileName = matchedPath.Path;
            process.StartInfo.UseShellExecute = true;//関連付けられたファイルを開く
            process.Start();
        }
    }
}
