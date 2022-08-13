using Moq;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherAppTest.Helper
{
    /// <summary>
    /// 指定したメッセージでMessageControlのDialogが呼び出されたかどうかチェックする
    /// Mockの検査がタイプ量多いため作成。
    /// </summary>
    internal static class DialogServiceHelper
    {
        internal static void VerifyMessageShowDialogIsCalled(Mock<IDialogService> mockService, string message)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.Add("Message", message);
            mockService.Verify(service => service.ShowDialog("MessageControl", parameters,  It.IsAny<Action<IDialogResult>>()), Times.Once);
        }
    }
}
