using LauncherModelLib;
using LauncherModelLib.PathModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal class RunnerService : IRunnerService
    {
        public void Run(IPath matchedPath)
        {
            ProcessRunner.Run(matchedPath);
        }
    }
}
