using AutoCompleteTextBox.Editors;
using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.Views;
using MLauncherIF;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MLauncherApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _dialogService;
        private IMessageService _messageService;
        private IRunnerService _runnerService;
        private IFilePathRepository _repository;

        private string _title = "MLauncher";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _textBoxTest;
        public string TextBoxText
        {
            get { return _textBoxTest; }
            set { SetProperty(ref _textBoxTest, value); }
        }
        public IPathSuggestionService PathSuggestionProvider { get; }
        public DelegateCommand<DragEventArgs> DragEnterCommand { get; }
        public DelegateCommand<DragEventArgs> DropCommand      { get; }
        public DelegateCommand RunCommand { get; }
        public DelegateCommand RunParentCommand { get; }

        public MainWindowViewModel(
            IMessageService messageService, IRunnerService runnerService, 
            IFilePathRepository filePathRepository, IDialogService dialogService,
            IPathSuggestionService suggestionProvider)
        {
            PathSuggestionProvider = suggestionProvider;

            _dialogService = dialogService;
            _messageService = messageService;
            _runnerService  = runnerService;
            _repository = filePathRepository;

            TextBoxText = "";

            DragEnterCommand    = new DelegateCommand<DragEventArgs>(MouseOverEvent);
            DropCommand         = new DelegateCommand<DragEventArgs>(DropEvent);
            RunCommand = new DelegateCommand(() => Execute(false));
            RunParentCommand = new DelegateCommand(() => Execute(true));
        }

        private void MouseOverEvent(DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void DropEvent(DragEventArgs e)
        {
            var textArray = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var text in textArray)
            {
                _repository.Save(new FilePath(text));
            }
        }

        /// <summary>
        /// テキストボックスに入力した内容を実行する
        /// </summary>
        /// <param name="callParent">指定したパスの親のパスを起動するオプション</param>
        private void Execute(bool parentCall)
        {
            ProcessUserInput(parentCall);
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            TextBoxText = "";
        }

        private void ProcessUserInput(bool parentCall)
        {
            if (TextBoxText == null) return;

            //特殊コマンド
            if (TextBoxText == "/list")
            {
                _runnerService.Run(_repository.ListCommandFile);
                return;
            }

            //通常ケース
            var matchedPathList = PathSuggestionProvider.GetPathSuggestions(TextBoxText);
            bool noHit = matchedPathList.Count == 0;
            if (noHit)
            {
                _messageService.ShowMessageBox("一致するパスが存在しません");
                return;
            }

            bool onlyOnePathHit = matchedPathList.Count == 1;
            if (onlyOnePathHit)
            {
                FilePath matchedPath = matchedPathList.First();
                var targetFilePath = (parentCall) ? matchedPath.ParentPath : matchedPath;
                _runnerService.Run(targetFilePath);
                return;
            }

            //複数ヒット
            var parameters = DialogParametersService.Create(nameof(PathListControlViewModel.PathList), matchedPathList);
            _dialogService.ShowDialog(nameof(PathListControl), parameters, (result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var filePathSelected = result.Parameters.GetValue<FilePath>(nameof(PathListControlViewModel.SelectedPathItem));
                    _runnerService.Run(filePathSelected);
                }
            });
        }
    }
}
