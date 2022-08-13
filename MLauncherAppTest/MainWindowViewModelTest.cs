using AutoCompleteTextBox.Editors;
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
        private Mock<IRunnerService> _runnerServiceMoc;
        private Mock<IFilePathRepository> _repositoryMoc;
        private Mock<IPathCandidateFilter> _suggestionService;

        private MainWindowViewModel _vm;

        public MainWindowViewModelTest()
        {
            _dialogServiceMoc = new Mock<IDialogService>();
            _runnerServiceMoc = new Mock<IRunnerService>();
            _repositoryMoc = new Mock<IFilePathRepository>();
            _suggestionService = new Mock<IPathCandidateFilter>();

            //Setup�ŏ㏑�����Ȃ���΁A������̃��|�W�g���Ƃ��Ă���
            _repositoryMoc.Setup(repo => repo.Load()).Returns(new List<FilePath>());
            _suggestionService.Setup(service => service.Filter(It.IsAny<string>())).Returns(new List<FilePath>());

            _vm = new MainWindowViewModel(_runnerServiceMoc.Object, _repositoryMoc.Object, _dialogServiceMoc.Object, _suggestionService.Object);
        }

        [Fact (Skip ="���t�@�N�^�����O�����Ă���")]
        public void ���X�g�ɑ��݂��Ȃ����O����͂�����_�t�@�C���p�X�Ƃ��ēo�^���邩�m�FDLG���o�ēo�^����()
        {
            _vm.TextBoxText = "\"" + @"C:\Directory\new_file.txt" + "\"";//�p�X�Ƃ��ăR�s�[�������́A�_�u���N�H�[�e�[�V�����ň͂܂ꂽ�p�X
            _vm.RunCommand.Execute();

            var parameter = new DialogParameters();
            parameter.Add("Message", $"�ȉ��̃p�X��o�^���܂����H\r\n{_vm.TextBoxText}");
            _dialogServiceMoc.Verify(service => service.ShowDialog("ConfirmControl", parameter, null), Times.Once);
        }

        [Fact]
        public void �������₪�Ȃ���Ԃ�Enter�������Ƃ��̃t�@�C�����J��()
        {
            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
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

            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
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
            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
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
            _suggestionService.Setup(suggestion => suggestion.Filter("Name"))
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
            _suggestionService.Setup(suggestion => suggestion.Filter("target"))
                .Returns(new List<FilePath>()
                {
                    new FilePath(@"C:\Dir\target.txt"),
                });

            _vm.TextBoxText = "target";
            _vm.RunCommand.Execute();

            Assert.Equal("", _vm.TextBoxText);
        }
    }
}