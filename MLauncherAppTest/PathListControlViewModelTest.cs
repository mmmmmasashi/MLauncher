using LauncherModelLib;
using MLauncherApp.ViewModels;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MLauncherAppTest
{
    public class PathListControlViewModelTest
    {
        [Fact]
        public void ファイルパスを選択した状態でRunのイベントが飛ぶとそのファイルパスが返される()
        {
            var vm = new PathListControlViewModel();
            vm.SelectedPathItem = new FilePath("filepath.txt");

            vm.RequestClose += new Action<IDialogResult>(result =>
            {
                var filePathSelected = result.Parameters.GetValue<FilePath>("SelectedPathItem");
                Assert.Equal(new FilePath("filepath.txt"), filePathSelected);
            });

            vm.RunSelectedItemCommand.Execute();
        }

    }
}
