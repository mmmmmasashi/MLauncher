using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLauncherApp.ViewModels
{
    public class ConfirmControlViewModel : BindableBase, IDialogAware
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _newPath;
        public string NewPath
        {
            get { return _newPath; }
            set { SetProperty(ref _newPath, value); }
        }

        public DelegateCommand OKCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public string Title => "確認";


        public event Action<IDialogResult> RequestClose;
        public ConfirmControlViewModel()
        {
            OKCommand = new DelegateCommand(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            });

            CancelCommand = new DelegateCommand(() => RequestClose.Invoke(new DialogResult(ButtonResult.Cancel)));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>(nameof(Message));
            NewPath = parameters.GetValue<string>(nameof(NewPath));
        }
    }
}
