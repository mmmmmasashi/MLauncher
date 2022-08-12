using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLauncherIF;

namespace MLauncherApp.Service
{
    internal class RunnerService : IRunnerService
    {
        public void Run(FilePath matchedPath)
        {
            ProcessRunner.Run(matchedPath);
        }
    }
}
