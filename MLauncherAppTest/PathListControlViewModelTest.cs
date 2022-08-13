using LauncherModelLib;
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
using MLauncherIF;

namespace MLauncherAppTest
{
    /// <summary>
    /// パス候補のリストウィンドウ
    /// </summary>
    public class PathListControlViewModelTest
    {
        private Mock<IFilePathRepository> _repository;
        private PathListControlViewModel vm;

        public PathListControlViewModelTest()
        {
            _repository = new Mock<IFilePathRepository>();
            vm = new PathListControlViewModel(_repository.Object);
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
            vm.SelectedPathItem = new FilePath("filepath.txt");
            vm.DeletePathCommand.Execute();
            _repository.Verify(repo => repo.Delete(new FilePath("filepath.txt")), Times.Once);
        }

    }
}
