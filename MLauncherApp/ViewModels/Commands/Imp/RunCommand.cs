using LauncherModelLib.Path.Existence;
using LauncherModelLib.Path.Paths;
using MLauncherApp.Service;
using MLauncherApp.Views;
using Prism.Services.Dialogs;
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
        private IPathJudgeService _pathJudgeService;
        private IDialogService _dialogService;

        public RunCommand(IPath filePath, bool parentCall, IRunnerService runnerService, IPathJudgeService pathJudgeService, IDialogService dialogService)
        {
            this._filePath = filePath;
            this._parentCall = parentCall;
            this._runnerService = runnerService;
            this._pathJudgeService = pathJudgeService;
            this._dialogService = dialogService;
        }

        void IUserCommand.Execute()
        {
            var targetFilePath = (_parentCall) ? _filePath.ParentPath : _filePath;
            if (_pathJudgeService.Exists(targetFilePath))
            {
                _runnerService.Run(targetFilePath);
            }
            else
            {
                _dialogService.ShowDialog(nameof(MessageControl), DialogParametersService.CreateForMessageControl("指定されたファイルが見つかりません"), null);
            }
        }
    }
}
