using LauncherModelLib;
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

        internal static DialogParameters Create(string key1, object value1, string key2, object value2)
        {
            var parameters = new DialogParameters();
            parameters.Add(key1, value1);
            parameters.Add(key2, value2);
            return parameters;
        }


        internal static IDialogParameters CreateForMessageControl(string message)
        {
            return Create(nameof(MessageControlViewModel.Message), message);
        }
    }
}
