using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.PathModel
{
    public static class PathFactory
    {
        public static IPath Create(string target)
        {
            if (target.StartsWith("http")) return new UrlPath(target);
            return new FilePath(target);
        }
    }
}
