using MLauncherApp.ViewModels.Commands.Imp;
using MLauncherAppTest.Helper;
using Moq;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MLauncherAppTest.Command
{
    public class AlreadyRegisteredCommandTest
    {
        [Fact]
        public void 既に登録されてるとダイアログを出すコマンド()
        {
            var dialogServiceMock = new Mock<IDialogService>();
            var command = new AlreadyRegisteredCommand(dialogServiceMock.Object);

            command.Execute();

            DialogServiceHelper.VerifyMessageShowDialogIsCalled(dialogServiceMock, "このパスは既に登録されています");
        }
    }
}
