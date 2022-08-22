using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path.Paths
{
    public interface IPath
    {
        string Path { get; }
        public IPath ParentPath { get; }
        bool Exists { get; }
    }
}
