using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.Views;
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

        public DelegateCommand<DragEventArgs> DragEnterCommand { get; }
        public DelegateCommand<DragEventArgs> DropCommand      { get; }
        public DelegateCommand<Key?>  KeyDownCommand   { get; }

        public MainWindowViewModel(IMessageService messageService, IRunnerService runnerService, IFilePathRepository filePathRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _messageService = messageService;
            _runnerService  = runnerService;
            _repository = filePathRepository;

            TextBoxText = "";

            DragEnterCommand    = new DelegateCommand<DragEventArgs>(MouseOverEvent);
            DropCommand         = new DelegateCommand<DragEventArgs>(DropEvent);
            KeyDownCommand      = new DelegateCommand<Key?>(KeyDownEvent);
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
        private void KeyDownEvent(Key? key)
        {
            if (key == null) return;
            if (key == Key.Return)
            {
                string userInput = TextBoxText;
                if (userInput == null) return;
                if (userInput == "/list")
                {
                    _runnerService.Run(_repository.FilePath);
                    return;
                }

                List<FilePath> matchedPathList = _repository.Search(userInput);
                if (matchedPathList.Count == 0)
                {
                    _messageService.ShowMessageBox("一致するパスが存在しません");
                    return;
                }
                else if (matchedPathList.Count == 1)
                {
                    _runnerService.Run(matchedPathList[0]);
                }
                else
                {
                    var parameters = new DialogParameters();
                    parameters.Add(nameof(PathListControlViewModel.PathList), matchedPathList);
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
    }
}
