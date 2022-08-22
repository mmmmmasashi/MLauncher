using LauncherModelLib.PathModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public interface IPathJudgeService
    {
        bool Exists(IPath path);
    }
}
