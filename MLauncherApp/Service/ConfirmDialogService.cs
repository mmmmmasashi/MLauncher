using MLauncherApp.ViewModels;
using MLauncherApp.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    /// <summary>
    /// 指定されたメッセージを使って、確認ダイアログを出し、OKかどうかを返すサービス
    /// </summary>
    internal class ConfirmDialogService : IConfirmDialogService
    {
        private readonly IDialogService _dialogService;

        internal ConfirmDialogService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public bool Confirm(string message)
        {
            bool isOKSelected = false;
            _dialogService.ShowDialog(
                nameof(ConfirmControl),
                DialogParametersService.Create(
                    nameof(ConfirmControlViewModel.Message), message),
                (result) =>
                {
                    isOKSelected = (result.Result == ButtonResult.OK);
                }
            );

            return isOKSelected;
        }
    }
}
