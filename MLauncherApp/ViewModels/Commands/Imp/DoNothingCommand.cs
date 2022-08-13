using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class DoNothingCommand : IUserCommand
    {
        void IUserCommand.Execute()
        {
            return;
        }
    }
}
