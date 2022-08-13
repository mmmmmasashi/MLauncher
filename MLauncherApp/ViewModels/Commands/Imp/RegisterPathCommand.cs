using LauncherModelLib;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class RegisterPathCommand : IUserCommand
    {
        private string _userInput;
        private IFilePathRepository _filePathRepository;
        private IDialogService _dialogService;

        public RegisterPathCommand(string userInput, IFilePathRepository filePathRepository, IDialogService dialogService)
        {
            this._userInput = userInput;
            this._filePathRepository = filePathRepository;
            this._dialogService = dialogService;
        }

        void IUserCommand.Execute()
        {
            throw new NotImplementedException();
            //    _dialogService.ShowDialog(
            //        nameof(MessageControl),
            //        DialogParametersService.Create(nameof(MessageControlViewModel.Message), "一致するパスが存在しません"),
            //        null
            //    );
            //    return;
        }
    }
}
