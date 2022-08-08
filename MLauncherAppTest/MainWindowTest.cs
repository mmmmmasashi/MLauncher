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
        [Fact]
        public void リストに存在しない名前を検索した時_存在しませんとエラーメッセージが出る()
        {
            var serviceMoc = new Mock<IMessageService>();
            var runnerServiceMoc = new Mock<IRunnerService>();
            var repositoryMoc = new Mock<IFilePathRepository>();
            var vm = new MainWindowViewModel(serviceMoc.Object, runnerServiceMoc.Object, repositoryMoc.Object);
            
            vm.TextBoxText = "not_exist_name";
            vm.KeyDownCommand.Execute(Key.Enter);

            serviceMoc.Verify(x => x.ShowMessageBox("一致するパスが存在しません"), Times.Once);
        }

        [Fact]
        public void スラッシュlistで全登録済パスを記録したテキストファイルを開く()
        {
            var messageServiceMoc = new Mock<IMessageService>();
            var runnerServiceMoc = new Mock<IRunnerService>();
            var repositoryMoc = new Mock<IFilePathRepository>();
            repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            var vm = new MainWindowViewModel(messageServiceMoc.Object, runnerServiceMoc.Object, repositoryMoc.Object);

            vm.TextBoxText = "/list";
            vm.KeyDownCommand.Execute(Key.Enter);

            runnerServiceMoc.Verify(x => x.Run(new FilePath("path_list.txt")), Times.Once);
        }
    }
}