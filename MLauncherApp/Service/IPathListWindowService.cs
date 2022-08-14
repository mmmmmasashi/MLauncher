using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal interface IPathListWindowService
    {
        void ShowDialog(IEnumerable<FilePath> candidates);
        void ShowDialog(IEnumerable<FilePath> candidates, string message);
    }
}
