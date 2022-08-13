﻿using LauncherModelLib;
using MLauncherApp.ViewModels;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal static class DialogParametersService
    {
        internal static DialogParameters Create(string key, object value)
        {
            var parameters = new DialogParameters();
            parameters.Add(key, value);
            return parameters;
        }

        internal static IDialogParameters CreateForMessageControl(string message)
        {
            return Create(nameof(MessageControlViewModel.Message), message);
        }
    }
}
