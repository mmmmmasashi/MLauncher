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
        public void ���X�g�ɑ��݂��Ȃ����O������������_���݂��܂���ƃG���[���b�Z�[�W���o��()
        {
            _repositoryMoc.Setup(repo => repo.Search(It.IsAny<string>())).Returns(new List<FilePath>());

            _vm.TextBoxText = "not_exist_name";
            _vm.KeyDownCommand.Execute(Key.Enter);

            _serviceMoc.Verify(x => x.ShowMessageBox("��v����p�X�����݂��܂���"), Times.Once);
        }

        [Fact]
        public void �X���b�V��list�őS�o�^�σp�X���L�^�����e�L�X�g�t�@�C�����J��()
        {
            _repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            _vm.TextBoxText = "/list";
            _vm.KeyDownCommand.Execute(Key.Enter);

            _runnerServiceMoc.Verify(x => x.Run(new FilePath("path_list.txt")), Times.Once);
        }

        [Fact]
        public void ������₪���鎞�̓t�@�C���p�X���X�g�E�B���h�E���J��()
        {
            //Name�Ō���������Name1.txt, Name2.txt�Ƀq�b�g����P�[�X
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