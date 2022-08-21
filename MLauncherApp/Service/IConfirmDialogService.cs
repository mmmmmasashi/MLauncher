using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal interface IConfirmDialogService
    {
        bool Confirm(string message);
    }
}
