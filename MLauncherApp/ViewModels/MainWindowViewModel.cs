using LauncherModelLib;
using MLauncherApp.Service;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;

namespace MLauncherApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
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

        public MainWindowViewModel(IMessageService messageService, IRunnerService runnerService, IFilePathRepository filePathRepository)
        {
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

                if(!_repository.AnyHit(userInput))
                {
                    _messageService.ShowMessageBox("一致するパスが存在しません");
                    return;
                }
                FilePath matchedPath = _repository.Search(userInput);
                _runnerService.Run(matchedPath);
            }
        }
    }
}
