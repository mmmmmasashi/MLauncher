using MLauncherApp.Service;
using MLauncherApp.ViewModels;
using Moq;
using System.Windows.Input;

namespace MLauncherAppTest
{
    public class UnitTest1
    {
        [Fact]
        public void リストに存在しない名前を検索した時_存在しませんとエラーメッセージが出る()
        {
            var serviceMoc = new Mock<IMessageService>();
            var vm = new MainWindowViewModel(serviceMoc.Object);
            
            vm.TextBoxText = "not_exist_name";
            vm.KeyDownCommand.Execute(Key.Enter);

            serviceMoc.Verify(x => x.ShowMessageBox("一致するパスが存在しません"), Times.Once);
        }
    }
}