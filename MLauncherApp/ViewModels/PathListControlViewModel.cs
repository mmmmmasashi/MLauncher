using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.Views;
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
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

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

        private readonly IFilePathRepository _filePathRepository;
        private readonly IDialogService _dialogService;

        public DelegateCommand RunParentOfSelectedItemCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public DelegateCommand LoadedCommand { get; }
        public DelegateCommand DeletePathCommand { get; }

        public PathListControlViewModel(IFilePathRepository filePathRepository, IDialogService dialogService)
        {
            _filePathRepository = filePathRepository;
            _dialogService = dialogService;

            filePathRepository.UpdateEvent += ReloadFilePath;

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

            DeletePathCommand = new DelegateCommand(RequestDelete);

            CancelCommand = new DelegateCommand(() => RequestClose.Invoke(new DialogResult(ButtonResult.Cancel)));
        }

        private void ReloadFilePath(object sender, EventArgs e)
        {
            var masterFilePathList = _filePathRepository.Load();

            //RepositoryとFilterキーワードから再生成する方法も考えたが、
            //全候補の表示で本Viewが呼び出されたときに困るため、マスターとの積集合を取ることにした
            var newFiles = masterFilePathList.Intersect(PathList).ToList();
            PathList.Clear();
            PathList.AddRange(newFiles);
        }

        /// <summary>
        /// 選択中のパスを確認してから削除する
        /// </summary>
        private void RequestDelete()
        {
            if (SelectedPathItem == null) return;

            var service = new ConfirmDialogService(_dialogService);
            var isOK = service.Confirm($"以下のパスを削除しますか？\r\n{SelectedPathItem.Path}");
            
            if (isOK)
            {
                _filePathRepository.Delete(SelectedPathItem);
            }
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
            Message = parameters.GetValue<string>(nameof(Message));
        }
    }
}
