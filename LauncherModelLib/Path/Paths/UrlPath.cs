using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path.Paths
{
    internal class UrlPath : IPath
    {
        private string _path;

        public UrlPath(string path)
        {
            if (!path.StartsWith("http")) throw new ArgumentException("URLではない文字列です");
            _path = path;
        }

        public string Path => _path;
        public string PathToRead
        {
            get
            {
                return System.Web.HttpUtility.UrlDecode(Path);
            }
        }


        public IPath ParentPath => new UrlPath(_path);

        public bool Exists => true;

        public override bool Equals(object? obj)
        {
            return obj is UrlPath path &&
                   _path == path._path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_path);
        }
    }
}
