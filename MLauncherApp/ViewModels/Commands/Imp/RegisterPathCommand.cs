using MLauncherApp.Service;
using MLauncherApp.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherModelLib.Path.Infra;
using LauncherModelLib.Path.Paths;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class RegisterPathCommand : IUserCommand
    {
        private string _userInput;
        private IPathRepository _filePathRepository;
        private IConfirmDialogService _confirmDialogService;

        public RegisterPathCommand(string userInput, IPathRepository filePathRepository, IDialogService dialogService)
        {
            this._userInput = userInput;
            this._filePathRepository = filePathRepository;
            this._confirmDialogService = new ConfirmDialogService(dialogService);
        }

        void IUserCommand.Execute()
        {
            var pathNew = PathFactory.Create(_userInput);
            var isOK = _confirmDialogService.Confirm($"以下のパスを登録しますか？\r\n{pathNew.PathToRead}");
            if (isOK) _filePathRepository.Save(pathNew);
            return;
        }
    }
}
