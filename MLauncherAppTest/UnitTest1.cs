using MLauncherApp.Service;
using MLauncherApp.ViewModels;
using Moq;
using System.Windows.Input;

namespace MLauncherAppTest
{
    public class UnitTest1
    {
        [Fact]
        public void ���X�g�ɑ��݂��Ȃ����O������������_���݂��܂���ƃG���[���b�Z�[�W���o��()
        {
            var serviceMoc = new Mock<IMessageService>();
            var vm = new MainWindowViewModel(serviceMoc.Object);
            
            vm.TextBoxText = "not_exist_name";
            vm.KeyDownCommand.Execute(Key.Enter);

            serviceMoc.Verify(x => x.ShowMessageBox("��v����p�X�����݂��܂���"), Times.Once);
        }
    }
}