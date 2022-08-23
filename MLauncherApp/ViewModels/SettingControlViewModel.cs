using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLauncherApp.ViewModels
{
    public class SettingControlViewModel : BindableBase, IDialogAware
    {
        public SettingControlViewModel()
        {

        }

        public string Title => "設定";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
