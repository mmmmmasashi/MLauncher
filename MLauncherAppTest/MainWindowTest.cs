using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.ViewModels;
using Moq;
using System;
using System.Windows.Input;
using Xunit;

namespace MLauncherAppTest
{
    public class MainWindowTest
    {
        private Mock<IMessageService> _serviceMoc;
        private Mock<IRunnerService> _runnerServiceMoc;
        private Mock<IFilePathRepository> _repositoryMoc;
        private MainWindowViewModel _vm;

        public MainWindowTest()
        {
            _serviceMoc = new Mock<IMessageService>();
            _runnerServiceMoc = new Mock<IRunnerService>();
            _repositoryMoc = new Mock<IFilePathRepository>();

            _repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            _vm = new MainWindowViewModel(_serviceMoc.Object, _runnerServiceMoc.Object, _repositoryMoc.Object);
        }

        [Fact]
        public void リストに存在しない名前を検索した時_存在しませんとエラーメッセージが出る()
        {
            _repositoryMoc.Setup(repo => repo.Search(It.IsAny<string>())).Returns(new List<FilePath>());

            _vm.TextBoxText = "not_exist_name";
            _vm.KeyDownCommand.Execute(Key.Enter);

            _serviceMoc.Verify(x => x.ShowMessageBox("一致するパスが存在しません"), Times.Once);
        }

        [Fact]
        public void スラッシュlistで全登録済パスを記録したテキストファイルを開く()
        {
            _repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            _vm.TextBoxText = "/list";
            _vm.KeyDownCommand.Execute(Key.Enter);

            _runnerServiceMoc.Verify(x => x.Run(new FilePath("path_list.txt")), Times.Once);
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
            _vm.KeyDownCommand.Execute(Key.Enter);

            _serviceMoc.Verify(messageService => messageService.ShowPathListWindow(new List<FilePath>
            {
                new FilePath(@"C:\Dir\Name1.txt"),
                new FilePath(@"C:\Dir\Name2.txt"),
            }), Times.Once);
        }
    }
}