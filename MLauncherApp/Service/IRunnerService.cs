using LauncherModelLib;
using MLauncherIF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    public interface IRunnerService
    {
        public void Run(FilePath matchedPath);
    }
}
