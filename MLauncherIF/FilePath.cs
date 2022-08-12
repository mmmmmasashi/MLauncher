using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherIF
{
    /// <summary>
    /// ViewがFilePath情報を求めているわけだから、ここでもいいのか？
    /// 
    /// <経緯>
    /// Modelに置いてあったIFilePathRepositoryがFilePathに依存しているのでModelのライブラリからここに移動してきた。
    /// ただ、個々が適切な置き場ではない気がしている。
    /// リファクタリングの途中過程として移動
    /// </summary>
    public class FilePath
    {
        public string Path { get; }
        public FilePath ParentPath { get => new FilePath(Directory.GetParent(Path)!.FullName); }

        public FilePath(string path)
        {
            this.Path = path;
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

        public bool Contains(string text)
        {
            return Path.Contains(text);
        }
    }
}
