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

            _repositoryMoc.Setup(repo => repo.FilePath).Returns(new FilePath("path_list.txt"));

            _vm = new MainWindowViewModel(_serviceMoc.Object, _runnerServiceMoc.Object, _repositoryMoc.Object, _dialogServiceMoc.Object);
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
        public void �������₪�Ȃ���Ԃ�Enter�������Ƃ��̃t�@�C�����J��()
        {
            _repositoryMoc.Setup(repo => repo.Search("target")).Returns(new List<FilePath>()
            {
                new FilePath(@"C:\Dir\target.txt"),
            });

            _vm.TextBoxText = "target";
            _vm.KeyDownCommand.Execute(Key.Enter);

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir\target.txt")));
        }

        //[Fact]
        //public void �������₪�Ȃ���Ԃ�Ctrl��Enter�𓯎���������Ƃ��̃t�@�C�����J��()
        //{
        //    _repositoryMoc.Setup(repo => repo.Search("target")).Returns(new List<FilePath>()
        //    {
        //        new FilePath(@"C:\Dir\target.txt"),
        //    });

        //    _vm.TextBoxText = "target";
        //    _vm.KeyDownCommand.Execute(Key.Enter);

        //    _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir\target.txt")));
        //}

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

            _dialogServiceMoc.Verify(
                service => service.ShowDialog(
                    "PathListControl",
                    It.IsAny<DialogParameters>(),
                    It.IsAny<Action<IDialogResult>>()
                    ),
                Times.Once);
        }
    }
}