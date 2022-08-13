using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class PathJudgeService : IPathJudgeService
    {
        /// <summary>
        /// TODO:将来的にはURL等にも対応する。その場合に更新していく
        /// </summary>
        public bool Exists(string path)
        {
            return (File.Exists(path) || Directory.Exists(path));
        }
    }
}
