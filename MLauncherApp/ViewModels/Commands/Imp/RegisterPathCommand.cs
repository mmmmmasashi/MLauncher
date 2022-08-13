using LauncherModelLib;
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
            _dialogService.ShowDialog(
                nameof(ConfirmControl),
                DialogParametersService.Create(
                    nameof(ConfirmControlViewModel.Message), $"以下のパスを登録しますか？\r\n{_userInput}"
                ),
                (result) =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        _filePathRepository.Save(new FilePath(_userInput));
                    }
                }
            );
            return;
        }
    }
}
