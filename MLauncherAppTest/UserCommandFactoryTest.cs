using LauncherModelLib.Path.Infra;
using MLauncherApp.Service;
using MLauncherApp.ViewModels.Commands;
using MLauncherApp.ViewModels.Commands.Imp;
using Moq;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LauncherModelLib.Path.Paths;
using LauncherModelLib.Path.Filter;
using LauncherModelLib.Path.Existence;

namespace MLauncherAppTest
{
    public class UserCommandFactoryTest
    {
        private Mock<IPathJudgeService> _pathJudgeService;
        private UserCommandFactory _factory;
        private Mock<IPathRepository> filePathRepository;
        private Mock<IPathCandidateFilter> pathCandidateFilter;

        public UserCommandFactoryTest()
        {
            filePathRepository = new Mock<IPathRepository>();
            pathCandidateFilter = new Mock<IPathCandidateFilter>();
            var dialogService = new Mock<IDialogService>();
            var runnerService = new Mock<IRunnerService>();
            var pathListWindowService = new Mock<IPathListWindowService>();
            _pathJudgeService = new Mock<IPathJudgeService>();

            _factory = new UserCommandFactory(
                filePathRepository.Object,
                pathCandidateFilter.Object,
                dialogService.Object,
                runnerService.Object,
                pathListWindowService.Object,
                _pathJudgeService.Object);
        }

        [Fact]
        public void 空文字列を入力したときは何もしない()
        {
            IUserCommand command = _factory.Create("", false);
            Assert.IsType<DoNothingCommand>(command);
        }

        [Fact]
        public void 未登録のパスを入力したら登録するか聞く()
        {
            //登録はされていない
            filePathRepository.Setup(repo => repo.Load()).Returns(new List<IPath>() { });

            //登録コマンドとなる
            IUserCommand command = _factory.Create(@"/reg C:\Dir\FileExists.txt", false);
            Assert.IsType<RegisterPathCommand>(command);
        }

        [Fact]
        public void 既に登録済のパスを入力したら_登録済ですとDLGで表示する()
        {
            //登録はすでにされている時
            filePathRepository.Setup(repo => repo.Load()).Returns(new List<IPath>() { new FilePath(@"C:\Dir\FileExists.txt") });

            //登録済と通知するコマンドとなる
            IUserCommand command = _factory.Create(@"/reg C:\Dir\FileExists.txt", false);
            Assert.IsType<AlreadyRegisteredCommand>(command);
        }
    }
}
