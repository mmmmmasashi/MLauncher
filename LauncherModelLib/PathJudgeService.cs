using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class PathJudgeService : IPathJudgeService
    {
        public bool Exists(string path)
        {
            if (File.Exists(path)) return true;
            return false;
        }
    }
}
