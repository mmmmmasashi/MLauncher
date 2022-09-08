using AutoCompleteTextBox.Editors;
using LauncherModelLib.Path.Existence;
using LauncherModelLib.Path.Filter;
using LauncherModelLib.Path.Infra;
using MLauncherApp.Service;
using MLauncherApp.ViewModels.Commands;
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
        private IRunnerService _runnerService;
        private UserCommandFactory _commandFactory;
        private IPathCandidateFilter _pathCandidateFilter;
        private IPathRepository _repository;
        private IPathListWindowService _pathListWindowService;
        private IPathJudgeService _pathJudgeService;

        private string _title = "MLauncher";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// ホットキーの登録に失敗した時Viewから通知する
        /// </summary>
        /// <param name="hotKeyRegisterException"></param>
        internal void NotifyFailureOfHotkey(Exception hotKeyRegisterException)
        {
            _dialogService.ShowDialog(nameof(MessageControl), DialogParametersService.CreateForMessageControl($"ホットキーの登録に失敗しました。({hotKeyRegisterException.Message})"), null);
        }

        private string _textBoxTest;
        public string TextBoxText
        {
            get { return _textBoxTest; }
            set { SetProperty(ref _textBoxTest, value); }
        }
        public ISuggestionProvider AutoCompleteProvider { get; }
        public DelegateCommand<DragEventArgs> DragEnterCommand { get; }
        public DelegateCommand<DragEventArgs> DropCommand      { get; }
        public DelegateCommand RunCommand { get; }
        public DelegateCommand RunParentCommand { get; }
        public DelegateCommand ShowSettingCommand { get; }

        public MainWindowViewModel(
            IRunnerService runnerService,
            IPathRepository filePathRepository, IDialogService dialogService,
            IPathCandidateFilter pathCandidateFilter, IPathJudgeService pathJudgeService)
        {
            _pathCandidateFilter = pathCandidateFilter;
            AutoCompleteProvider = new AutoCompleteProvider(pathCandidateFilter);

            _dialogService = dialogService;
            _runnerService = runnerService;
            _repository = filePathRepository;
            _pathListWindowService = new PathListWindowService(dialogService, runnerService);
            _pathJudgeService = pathJudgeService;

            TextBoxText = "";

            DragEnterCommand = new DelegateCommand<DragEventArgs>(MouseOverEvent);
            DropCommand = new DelegateCommand<DragEventArgs>(DropEvent);
            RunCommand = new DelegateCommand(() => Execute(false));
            RunParentCommand = new DelegateCommand(() => Execute(true));
            ShowSettingCommand = new DelegateCommand(() => _dialogService.ShowDialog(nameof(SettingControl), null, null));

            _commandFactory = new UserCommandFactory(
                filePathRepository, pathCandidateFilter, 
                dialogService, runnerService, 
                _pathListWindowService, pathJudgeService);

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
                var command = _commandFactory.CreateRegisterCommand(text);
                command.Execute();
            }
        }

        /// <summary>
        /// テキストボックスに入力した内容を実行する
        /// </summary>
        /// <param name="callParent">指定したパスの親のパスを起動するオプション</param>
        private void Execute(bool parentCall)
        {
            IUserCommand command = _commandFactory.Create(TextBoxText, parentCall);
            command.Execute();
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            TextBoxText = "";
        }
    }


    //TODO:削除？使ってない？
    /// <summary>
    /// ホットキーの登録成功/失敗を保持するクラス
    /// </summary>
    class HotKeyResult
    {
        internal bool IsFailed => !succeededToRegisterHotKey;
        private bool succeededToRegisterHotKey = true;
        private Exception _hotKeyException = null;

        private HotKeyResult(bool succeededToRegisterHotKey, Exception hotKeyException = null)
        {
            this.succeededToRegisterHotKey = succeededToRegisterHotKey;
            _hotKeyException = hotKeyException;
        }

        internal static HotKeyResult Failed(Exception e)
        {
            return new HotKeyResult(false, e);
        }

        internal static HotKeyResult Succeeded()
        {
            return new HotKeyResult(true, null);
        }
    }
}
