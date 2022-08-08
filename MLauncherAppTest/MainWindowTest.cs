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
        public void ���X�g�ɑ��݂��Ȃ����O������������_���݂��܂���ƃG���[���b�Z�[�W���o��()
        {
            var serviceMoc = new Mock<IMessageService>();
            var runnerServiceMoc = new Mock<IRunnerService>();
            var repositoryMoc = new Mock<IFilePathRepository>();
            var vm = new MainWindowViewModel(serviceMoc.Object, runnerServiceMoc.Object, repositoryMoc.Object);
            
            vm.TextBoxText = "not_exist_name";
            vm.KeyDownCommand.Execute(Key.Enter);

            serviceMoc.Verify(x => x.ShowMessageBox("��v����p�X�����݂��܂���"), Times.Once);
        }

        [Fact]
        public void �X���b�V��list�őS�o�^�σp�X���L�^�����e�L�X�g�t�@�C�����J��()
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