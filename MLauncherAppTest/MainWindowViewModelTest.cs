using AutoCompleteTextBox.Editors;
using LauncherModelLib;
using LauncherModelLib.Infra;
using LauncherModelLib.PathModel;
using MLauncherApp.Service;
using MLauncherApp.ViewModels;
using Moq;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Xunit;

namespace MLauncherAppTest
{
    public class MainWindowViewModelTest
    {
        private Mock<IDialogService> _dialogServiceMoc;
        private Mock<IRunnerService> _runnerServiceMoc;
        private Mock<IPathRepository> _repositoryMoc;
        private Mock<IPathCandidateFilter> _suggestionService;
        private Mock<IPathJudgeService> _pathService;

        private MainWindowViewModel _vm;

        public MainWindowViewModelTest()
        {
            _dialogServiceMoc = new Mock<IDialogService>();
            _runnerServiceMoc = new Mock<IRunnerService>();
            _repositoryMoc = new Mock<IPathRepository>();
            _suggestionService = new Mock<IPathCandidateFilter>();
            _pathService = new Mock<IPathJudgeService>();

            //Setupで上書きしなければ、何も空のリポジトリとしておく
            _repositoryMoc.Setup(repo => repo.Load()).Returns(new List<IPath>());
            _suggestionService.Setup(service => service.Filter(It.IsAny<string>())).Returns(new List<IPath>());

            _vm = new MainWindowViewModel(
                _runnerServiceMoc.Object,
                _repositoryMoc.Object,
                _dialogServiceMoc.Object,
                _suggestionService.Object,
                _pathService.Object);
        }

        [Fact]
        public void リストに存在しない名前を入力した時_ファイルパスでなければ_存在しませんとエラーメッセージが出る()
        {
            _pathService.Setup(pathService => pathService.Exists(new FilePath("not_exist_name"))).Returns(false);

            _suggestionService.Setup(suggestion => suggestion.Filter("not_exist_name")).Returns(new List<IPath>());

            _vm.TextBoxText = "not_exist_name";
            _vm.RunCommand.Execute();

            var parameter = new DialogParameters();
            parameter.Add("Message", "一致するパスが存在しません");
            _dialogServiceMoc.Verify(service => service.ShowDialog("MessageControl", parameter, null), Times.Once);
        }

        [Fact]
        public void リストに存在しない名前を入力した時_ファイルパスとして登録するか確認DLGが出て登録する()
        {
            //新しいファイルが存在すると返すように用意
            _pathService.Setup(pathService => pathService.Exists(new FilePath(@"C:\Directory\new_file.txt"))).Returns(true);

            //メッセージ付きで呼ばれることをチェック
            _dialogServiceMoc.Setup(service => service.ShowDialog(
                "ConfirmControl",
                It.IsAny<IDialogParameters>(),
                It.IsAny<Action<IDialogResult>>()
                )).Callback<string, IDialogParameters, Action<IDialogResult>>((name, parameters, callback) =>
                {
                    Assert.Equal("以下のパスを登録しますか？\r\n" + @"C:\Directory\new_file.txt", parameters.GetValue<string>("Message"));
                    callback(new DialogResult(ButtonResult.OK));
                });

            _vm.TextBoxText = @"C:\Directory\new_file.txt";

            _vm.RunCommand.Execute();

            _dialogServiceMoc.Verify(service => service.ShowDialog(
                "ConfirmControl", 
                It.IsAny<IDialogParameters>(),
                It.IsAny<Action<IDialogResult>>()
                ),
                Times.Once);

            //新規に登録されていること
            _repositoryMoc.Verify(repo => repo.Save(new FilePath(@"C:\Directory\new_file.txt")));
        }

        [Fact]
        public void 一つしか候補がない状態でEnterを押すとそのファイルを開く()
        {
            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
                .Returns(new List<IPath>()
                {
                    new FilePath(@"C:\Dir\target.txt"),
                });

            _vm.TextBoxText = "target";
            _vm.RunCommand.Execute();

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir\target.txt")));
        }

        [Fact]
        public void ヒットしたパスがファイルパスの時に_CtrlとEnterを同時押しすると親ディレクトリを開く()
        {

            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
                .Returns(new List<IPath>()
                {
                    new FilePath(@"C:\Dir\target.txt"),
                });

            _vm.TextBoxText = "target";
            _vm.RunParentCommand.Execute();

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir")));
        }

        [Fact]
        public void ヒットしたパスがディレクトリパスの時に_CtrlとEnterを同時押しすると親ディレクトリを開く()
        {
            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
                .Returns(new List<IPath>()
                {
                    new FilePath(@"C:\Dir\SubDir"),
                });

            _vm.TextBoxText = "target";
            _vm.RunParentCommand.Execute();

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir")));
        }

        [Fact]
        public void 複数候補がある時はファイルパスリストウィンドウを開く()
        {
            //Nameで検索したらName1.txt, Name2.txtにヒットするケース
            _suggestionService.Setup(suggestion => suggestion.Filter("Name"))
                .Returns(new List<IPath>()
                {
                    new FilePath(@"C:\Dir\Name1.txt"),
                    new FilePath(@"C:\Dir\Name2.txt"),
                });

            _vm.TextBoxText = "Name";
            _vm.RunCommand.Execute();

            _dialogServiceMoc.Verify(
                service => service.ShowDialog(
                    "PathListControl",
                    It.IsAny<DialogParameters>(),
                    It.IsAny<Action<IDialogResult>>()
                    ),
                Times.Once);
        }

        [Fact]
        public void スラッシュallで全登録済パスを記録したテキストファイルを開く()
        {
            _repositoryMoc.Setup(repo => repo.Load()).Returns(new List<IPath>()
            {
                new FilePath("file1.txt"),
                new FilePath("file2.txt"),
            });

            _dialogServiceMoc.Setup(service => service.ShowDialog(
                "PathListControl",
                It.IsAny<IDialogParameters>(),
                It.IsAny<Action<IDialogResult>>()))
                .Callback<string, IDialogParameters, Action<IDialogResult>>((name, parameters, dialogResult) =>
                {
                    IEnumerable<IPath> paths = parameters.GetValue<IEnumerable<IPath>>("PathList");
                    Assert.Equal(new List<IPath>()
                    {
                                    new FilePath("file1.txt"),
                                    new FilePath("file2.txt"),
                    }, paths);
                });

            _vm.TextBoxText = "/all";
            _vm.RunCommand.Execute();

        }

        [Fact]
        public void Enterを押すとテキストボックスはクリアされる_処理が完了するため()
        {
            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
                .Returns(new List<IPath>()
                {
                    new FilePath(@"C:\Dir\target.txt"),
                });

            _vm.TextBoxText = "target";
            _vm.RunCommand.Execute();

            Assert.Equal("", _vm.TextBoxText);
        }
    }
}