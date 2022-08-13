using AutoCompleteTextBox.Editors;
using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.ViewModels;
using MLauncherIF;
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
        private Mock<IPathSuggestionService> _suggestionService;

        private MainWindowViewModel _vm;

        public MainWindowViewModelTest()
        {
            _dialogServiceMoc = new Mock<IDialogService>();
            _serviceMoc = new Mock<IMessageService>();
            _runnerServiceMoc = new Mock<IRunnerService>();
            _repositoryMoc = new Mock<IFilePathRepository>();
            _suggestionService = new Mock<IPathSuggestionService>();

            //Setup�ŏ㏑�����Ȃ���΁A������̃��|�W�g���Ƃ��Ă���
            _repositoryMoc.Setup(repo => repo.Load()).Returns(new List<FilePath>());
            _suggestionService.Setup(service => service.GetPathSuggestions(It.IsAny<string>())).Returns(new List<FilePath>());

            _vm = new MainWindowViewModel(_serviceMoc.Object, _runnerServiceMoc.Object, _repositoryMoc.Object, _dialogServiceMoc.Object, _suggestionService.Object);
        }

        [Fact]
        public void ���X�g�ɑ��݂��Ȃ����O������������_���݂��܂���ƃG���[���b�Z�[�W���o��()
        {
            _suggestionService.Setup(suggestion => suggestion.GetPathSuggestions("not_exist_name")).Returns(new List<FilePath>());

            _vm.TextBoxText = "not_exist_name";
            _vm.RunCommand.Execute();

            _serviceMoc.Verify(x => x.ShowMessageBox("��v����p�X�����݂��܂���"), Times.Once);
        }

        [Fact]
        public void �������₪�Ȃ���Ԃ�Enter�������Ƃ��̃t�@�C�����J��()
        {
            _suggestionService.Setup(suggestion => suggestion.GetPathSuggestions("target"))
                .Returns(new List<FilePath>()
                {
                    new FilePath(@"C:\Dir\target.txt"),
                });

            _vm.TextBoxText = "target";
            _vm.RunCommand.Execute();

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir\target.txt")));
        }

        [Fact]
        public void �q�b�g�����p�X���t�@�C���p�X�̎���_Ctrl��Enter�𓯎���������Ɛe�f�B���N�g�����J��()
        {

            _suggestionService.Setup(suggestion => suggestion.GetPathSuggestions("target"))
                .Returns(new List<FilePath>()
                {
                    new FilePath(@"C:\Dir\target.txt"),
                });

            _vm.TextBoxText = "target";
            _vm.RunParentCommand.Execute();

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir")));
        }

        [Fact]
        public void �q�b�g�����p�X���f�B���N�g���p�X�̎���_Ctrl��Enter�𓯎���������Ɛe�f�B���N�g�����J��()
        {
            _suggestionService.Setup(suggestion => suggestion.GetPathSuggestions("target"))
                .Returns(new List<FilePath>()
                {
                    new FilePath(@"C:\Dir\SubDir"),
                });

            _vm.TextBoxText = "target";
            _vm.RunParentCommand.Execute();

            _runnerServiceMoc.Verify(runner => runner.Run(new FilePath(@"C:\Dir")));
        }

        [Fact]
        public void ������₪���鎞�̓t�@�C���p�X���X�g�E�B���h�E���J��()
        {
            //Name�Ō���������Name1.txt, Name2.txt�Ƀq�b�g����P�[�X
            _suggestionService.Setup(suggestion => suggestion.GetPathSuggestions("Name"))
                .Returns(new List<FilePath>()
                {
                    new FilePath(@"C:\Dir\Name1.txt"),
                    new FilePath(@"C:\Dir\Name2.txt"),
                });

            _vm.TextBoxText = "Name";
            _vm.RunCommand.Execute();

            _dialogServiceMoc.Verify(
                service => service.ShowDialog(
                    "PathListControl",
                    It.IsAny<DialogParameters>(),
                    It.IsAny<Action<IDialogResult>>()
                    ),
                Times.Once);
        }

        [Fact]
        public void �X���b�V��all�őS�o�^�σp�X���L�^�����e�L�X�g�t�@�C�����J��()
        {
            _repositoryMoc.Setup(repo => repo.Load()).Returns(new List<FilePath>()
            {
                new FilePath("file1.txt"),
                new FilePath("file2.txt"),
            });

            _dialogServiceMoc.Setup(service => service.ShowDialog(
                "PathListControl",
                It.IsAny<IDialogParameters>(),
                It.IsAny<Action<IDialogResult>>()))
                .Callback<string, IDialogParameters, Action<IDialogResult>>((name, parameters, dialogResult) =>
                {
                    IEnumerable<FilePath> paths = parameters.GetValue<IEnumerable<FilePath>>("PathList");
                    Assert.Equal(new List<FilePath>()
                    {
                                    new FilePath("file1.txt"),
                                    new FilePath("file2.txt"),
                    }, paths);
                });

            _vm.TextBoxText = "/all";
            _vm.RunCommand.Execute();

        }

        [Fact]
        public void Enter�������ƃe�L�X�g�{�b�N�X�̓N���A�����_�������������邽��()
        {
            _vm.TextBoxText = "some_text";
            _vm.RunCommand.Execute();
            Assert.Equal("", _vm.TextBoxText);
        }
    }
}