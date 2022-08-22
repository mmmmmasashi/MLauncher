using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path.Paths
{
    internal class FilePath : IPath
    {
        private readonly string DoubleQuatation = "\"";
        public string Path { get; }
        public IPath ParentPath { get => PathFactory.Create(Directory.GetParent(Path)!.FullName); }
        public bool Exists { get => File.Exists(Path) || Directory.Exists(Path); }

        internal FilePath(string path)
        {
            //Windowsのエクスプローラーでパスをコピーすると、ダブルクォーテーションで囲まれたパスが取得されるので
            //それは削除する
            path = RemoveDoubleQuatationIfWrapped(path);
            Path = path;
        }

        /// <summary>
        /// ダブルクォーテーションで囲まれた文字列であれば、除去する
        /// </summary>
        private string RemoveDoubleQuatationIfWrapped(string path)
        {
            if (path.StartsWith(DoubleQuatation) && path.EndsWith(DoubleQuatation))
            {
                return path.Substring(1, path.Length - 2);
            }
            return path;
        }

        public override bool Equals(object? obj)
        {
            return obj is FilePath path &&
                   Path == path.Path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path);
        }

    }
}
