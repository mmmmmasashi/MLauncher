using LauncherModelLib;
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
        public DelegateCommand<Key?> DataGridKeyDownCommand { get; }

        public PathListControlViewModel()
        {
            DataGridKeyDownCommand = new DelegateCommand<Key?>(DataGridKeyEvent);
        }

        private void DataGridKeyEvent(Key? key)
        {
            if (key == null) return;
            if (key != Key.Enter) return;

            //TODO:以下メソッドとかにまとめたい？
            DialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add(nameof(SelectedPathItem), SelectedPathItem);
            DialogResult result = new DialogResult(ButtonResult.OK, dialogParameters);

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
