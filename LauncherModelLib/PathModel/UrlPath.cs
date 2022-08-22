using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.PathModel
{
    internal class UrlPath : IPath
    {
        private string _path;

        public UrlPath(string path)
        {
            if (!path.StartsWith("http")) throw new ArgumentException("URLではない文字列です");
            this._path = path;
        }

        public string Path => throw new NotImplementedException();

        public IPath ParentPath => throw new NotImplementedException();

        public bool Exists => throw new NotImplementedException();
    }
}
