using LauncherModelLib;
using MLauncherApp.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MLauncherApp.ViewModels
{
    public class PathListControlViewModel : BindableBase, IDialogAware
    {
        public ObservableCollection<FilePath> PathList { get; } = new ObservableCollection<FilePath>();
        
        private FilePath _selectedPathItem;
        public FilePath SelectedPathItem
        {
            get { return _selectedPathItem; }
            set { SetProperty(ref _selectedPathItem, value); }
        }

        public bool IsSelected { get; } = false;

        public string Title => "パス選択";
        public DelegateCommand RunSelectedItemCommand { get; }
        public DelegateCommand RunParentOfSelectedItemCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public DelegateCommand LoadedCommand { get; }

        public PathListControlViewModel()
        {
            RunParentOfSelectedItemCommand = new DelegateCommand(() =>
            {
                if (SelectedPathItem == null) return;
                ReturnPath(SelectedPathItem.ParentPath);
            });

            RunSelectedItemCommand = new DelegateCommand(() =>
            {
                if (SelectedPathItem == null) return;
                ReturnPath(SelectedPathItem);
            });

            LoadedCommand = new DelegateCommand(() =>
            {
                if (PathList.Count > 0)
                {
                    SelectedPathItem = PathList.First();
                }
            });
            
            CancelCommand = new DelegateCommand(() => RequestClose.Invoke(new DialogResult(ButtonResult.Cancel)));
        }

        /// <summary>
        /// 指定されたパスを呼び元に返してこのウィンドウはクローズする
        /// </summary>
        /// <param name="filePath"></param>
        private void ReturnPath(FilePath filePath)
        {
            if (filePath == null) return;

            var parameters = DialogParametersService.Create(nameof(SelectedPathItem), filePath);
            DialogResult result = new DialogResult(ButtonResult.OK, parameters);

            RequestClose.Invoke(result);
        }

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
            PathList.AddRange(parameters.GetValue<List<FilePath>>(nameof(PathList)));
        }
    }
}
