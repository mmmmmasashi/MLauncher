using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path.Existence
{
    /// <summary>
    /// 不要かもしれない。
    /// 実装過程で生まれて不要になったので消そうと思ったが、
    /// FilePathの存在有無をテストする際にMockを使っており、
    /// そのために残している。
    /// </summary>
    public class PathJudgeService : IPathJudgeService
    {
        public bool Exists(IPath path)
        {
            return path.Exists;
        }
    }
}
