using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class FilePathTest
    {
        private FilePath _filePath;

        public FilePathTest()
        {
            _filePath = new FilePath(@"C:\directory\sample.txt");
        }

        [Fact]
        public void Pathはその文字列そのまま()
        {
            Assert.Equal(@"C:\directory\sample.txt", _filePath.Path);
        }

        [Fact]
        public void PathToReadもそのまま()
        {
            //ファイルパスはパーセントエンコーディングは想定していない
            Assert.Equal(@"C:\directory\sample.txt", _filePath.PathToRead);
        }
    }
}
