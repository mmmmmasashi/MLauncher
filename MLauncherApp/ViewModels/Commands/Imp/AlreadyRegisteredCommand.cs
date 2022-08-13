using MLauncherApp.Service;
using MLauncherApp.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class AlreadyRegisteredCommand : IUserCommand
    {

        IDialogService service;
        public AlreadyRegisteredCommand(IDialogService service)
        {
            this.service = service;
        }


        public void Execute()
        {
            service.ShowDialog(nameof(MessageControl), DialogParametersService.CreateForMessageControl("このパスは既に登録されています"), null);
        }
    }
}
