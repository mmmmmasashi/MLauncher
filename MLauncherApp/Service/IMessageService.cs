using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    public interface IMessageService
    {
        void ShowMessageBox(string message);
        void ShowPathListWindow(List<FilePath> filePaths);
    }
}
