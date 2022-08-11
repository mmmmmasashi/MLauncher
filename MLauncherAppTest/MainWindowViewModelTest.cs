using LauncherModelLib;
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
        private Mock<IMessageService> _serviceMoc;
        private Mock<IRunnerService> _runnerServiceMoc;
        private Mock<IFilePathRepository> _repositoryMoc;
        private MainWindowViewModel _vm;

        public MainWindowViewModelTest()
        {
            _dialogServiceMoc = new Mock<IDialogService>();
            _serviceMoc = new Mock<IMessageService>();
            _runnerServiceMoc = new Mock<IRunnerService>();
            _repositoryMoc = new Mock<IFilePathRepository>();

            //Setupで上書きしなければ、何も空のリポジトリとしておく
            _repositoryMoc.Setup(repo => repo.Search(It.IsAny<string>())).Returns(new List<FilePath>());

            //listコマンド用にパスを用意しておく
            _repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            _vm = new MainWindowViewModel(_serviceMoc.Object, _runnerServiceMoc.Object, _repositoryMoc.Object, _dialogServiceMoc.Object);
        }

        [Fact]
        public void リストに存在しない名前を検索した時_存在しませんとエラーメッセージが出る()
        {
            _repositoryMoc.Setup(repo => repo.Search(It.IsAny<string>())).Returns(new List<FilePath>());

            _vm.TextBoxText = "not_exist_name";
            _vm.RunCommand.Execute();

            _serviceMoc.Verify(x => x.ShowMessageBox("一致するパスが存在しません"), Times.Once);
        }

        [Fact]
        public void スラッシュlistで全登録済パスを記録したテキストファイルを開く()
        {
            _repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            _vm.TextBoxText = "/list";
            _vm.RunCommand.Execute();

            _runnerServiceMoc.Verify(x => x.Run(new FilePath("path_list.txt")), Times.Once);
        }

        [Fact]
        public void 一つしか候補がない状態でEnterを押すとそのファイルを開く()
        {
            _repositoryMoc.Setup(repo => repo.Search("target")).Returns(new List<FilePath>()
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
            _repositoryMoc.Setup(repo => repo.Search("target")).Returns(new List<FilePath>()
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
            _repositoryMoc.Setup(repo => repo.Search("target")).Returns(new List<FilePath>()
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
            _repositoryMoc.Setup(repo => repo.Search("Name")).Returns(new List<FilePath>()
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
        public void Enterを押すとテキストボックスはクリアされる_処理が完了するため()
        {
            _vm.TextBoxText = "some_text";
            _vm.RunCommand.Execute();
            Assert.Equal("", _vm.TextBoxText);

        }
    }
}