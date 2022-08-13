﻿using LauncherModelLib;
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
    internal class PathListWindowService
    {
        private IDialogService _dialogService;
        private IRunnerService _runnerService;

        internal PathListWindowService(IDialogService dialogService, IRunnerService runnerService)
        {
            _dialogService = dialogService;
            _runnerService = runnerService;
        }

        internal void ShowDialog(IEnumerable<FilePath> candidates)
        {
            var parameters = DialogParametersService.Create(nameof(PathListControlViewModel.PathList), candidates);
            _dialogService.ShowDialog(nameof(PathListControl), parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var filePathSelected = result.Parameters.GetValue<FilePath>(nameof(PathListControlViewModel.SelectedPathItem));
                    _runnerService.Run(filePathSelected);
                }
            });
            return;
        }
    }
}