using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal interface IPathListWindowService
    {
        void ShowDialog(IEnumerable<IPath> candidates);
        void ShowDialog(IEnumerable<IPath> candidates, string message);
    }
}
