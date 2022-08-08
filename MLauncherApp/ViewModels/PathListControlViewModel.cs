using LauncherModelLib;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MLauncherApp.ViewModels
{
    public class PathListControlViewModel : BindableBase, IDialogAware
    {
        public ObservableCollection<FilePath> PathList { get; } = new ObservableCollection<FilePath>();
        public bool IsSelected { get; } = false;
        public FilePath SelectedPath { get; }

        public string Title => "パス選択";

        public PathListControlViewModel()
        {
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
