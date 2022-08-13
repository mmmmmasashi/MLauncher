using LauncherModelLib;
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

namespace MLauncherAppTest
{
    public class UserCommandFactoryTest
    {
        private UserCommandFactory _factory;

        public UserCommandFactoryTest()
        {
            var filePathRepository = new Mock<IFilePathRepository>();
            var pathCandidateFilter = new Mock<IPathCandidateFilter>();
            var dialogService = new Mock<IDialogService>();
            var runnerService = new Mock<IRunnerService>();
            var pathListWindowService = new Mock<IPathListWindowService>();
            var pathJudgeService = new Mock<IPathJudgeService>();

            _factory = new UserCommandFactory(
                filePathRepository.Object,
                pathCandidateFilter.Object,
                dialogService.Object,
                runnerService.Object,
                pathListWindowService.Object,
                pathJudgeService.Object);
        }

        [Fact]
        public void 空文字列を入力したときは何もしない()
        {
            IUserCommand command = _factory.Create("", false);
            Assert.IsType<DoNothingCommand>(command);
        }
    }
}
