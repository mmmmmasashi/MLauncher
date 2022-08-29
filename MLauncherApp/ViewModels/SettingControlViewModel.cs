using MLauncherApp.Setting;
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
        private string _settingFilePath;
        public string SettingFilePath
        {
            get { return _settingFilePath; }
            set { SetProperty(ref _settingFilePath, value); }
        }

        public SettingControlViewModel(ISettingRepository settingSepository)
        {
            AppSetting setting = settingSepository.Load();
            SettingFilePath = setting.SettingFilePath;
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
