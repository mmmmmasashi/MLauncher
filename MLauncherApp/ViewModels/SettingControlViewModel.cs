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
        private readonly ISettingRepository _settingRepository;

        public string SettingFilePath
        {
            get { return _settingFilePath; }
            set { SetProperty(ref _settingFilePath, value); }
        }

        public DelegateCommand CancelCommand { get;}
        public DelegateCommand SaveAndCloseCommand { get; }

        public SettingControlViewModel(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
            AppSetting setting = settingRepository.Load();
            SettingFilePath = setting.SettingFilePath;

            SaveAndCloseCommand = new DelegateCommand(SaveAndClose);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        private void SaveAndClose()
        {
            var newSetting = new AppSetting(SettingFilePath);
            _settingRepository.Save(newSetting);
            RequestClose.Invoke(new DialogResult(ButtonResult.OK));
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
