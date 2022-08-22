using LauncherModelLib.Path.Paths;
using MLauncherApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class RunCommand : IUserCommand
    {
        private IPath _filePath;
        private bool _parentCall;
        private IRunnerService _runnerService;

        public RunCommand(IPath filePath, bool parentCall, Service.IRunnerService runnerService)
        {
            this._filePath = filePath;
            this._parentCall = parentCall;
            this._runnerService = runnerService;
        }

        void IUserCommand.Execute()
        {
            var targetFilePath = (_parentCall) ? _filePath.ParentPath : _filePath;
            _runnerService.Run(targetFilePath);
            return;
        }
    }
}
