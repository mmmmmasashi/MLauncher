using LauncherModelLib.Infra;
using LauncherModelLib.PathModel;
using MLauncherApp.ViewModels;
using MLauncherApp.Views;
using Moq;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MLauncherAppTest
{
    /// <summary>
    /// パス候補のリストウィンドウ
    /// </summary>
    public class PathListControlViewModelTest
    {
        private Mock<IPathRepository> _repository;
        private readonly Mock<IDialogService> _dialogService;
        private PathListControlViewModel vm;

        public PathListControlViewModelTest()
        {
            _repository = new Mock<IPathRepository>();
            _dialogService = new Mock<IDialogService>();
            vm = new PathListControlViewModel(_repository.Object, _dialogService.Object);
        }

        [Fact]
        public void ファイルパスを選択した状態でEnterを押すとそのファイルパスが返される()
        {
            vm.SelectedPathItem = new FilePath("filepath.txt");

            vm.RequestClose += new Action<IDialogResult>(result =>
            {
                var filePathSelected = result.Parameters.GetValue<FilePath>("SelectedPathItem");
                Assert.Equal(new FilePath("filepath.txt"), filePathSelected);
            });

            vm.RunSelectedItemCommand.Execute();
        }

        [Fact]
        public void ファイルパスを選択した状態でCTRL_Enterを押すとその親のディレクトリのパスが返される()
        {
            vm.SelectedPathItem = new FilePath(@"C:\parent\filepath.txt");

            vm.RequestClose += new Action<IDialogResult>(result =>
            {
                var filePathSelected = result.Parameters.GetValue<FilePath>("SelectedPathItem");
                Assert.Equal(new FilePath(@"C:\parent"), filePathSelected);
            });

            vm.RunParentOfSelectedItemCommand.Execute();
        }

        [Fact]
        public void ファイルパスを選択した状態でDeleteを押すと_そのファイルパスが削除される()
        {
            _dialogService.Setup(service => service.ShowDialog("ConfirmControl", It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()))
                .Callback<string, IDialogParameters, Action<IDialogResult>>((name, parameters, resultAction) => resultAction(new DialogResult(ButtonResult.OK)));
            vm.SelectedPathItem = new FilePath("filepath.txt");
            vm.DeletePathCommand.Execute();
            _dialogService.Verify(service => service.ShowDialog("ConfirmControl", It.IsAny<IDialogParameters>(), It.IsAny<Action<IDialogResult>>()));
            _repository.Verify(repo => repo.Delete(new FilePath("filepath.txt")), Times.Once);
        }

    }
}
