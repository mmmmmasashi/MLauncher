using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path
{
    public interface IPath
    {
        bool Exists { get; }
    }
}
