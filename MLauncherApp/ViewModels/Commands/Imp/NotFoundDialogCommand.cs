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
    internal class NotFoundDialogCommand : IUserCommand
    {
        private IDialogService _dialogService;

        public NotFoundDialogCommand(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }

        void IUserCommand.Execute()
        {
            _dialogService.ShowDialog(
                nameof(MessageControl),
                DialogParametersService.Create(nameof(MessageControlViewModel.Message), "一致するパスが存在しません"),
                null
            );
        }
    }
}
