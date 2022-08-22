using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path.Existence
{
    /// <summary>
    /// ファイルの存在有無チェックのサービス
    /// </summary>
    /// <remarks>
    /// なくてもよいように思うが、テストの関係で消せていない
    /// </remarks>
    public interface IPathJudgeService
    {
        bool Exists(IPath path);
    }
}
