using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class PathFactoryTest
    {
        [Fact]
        public void http_スタートだとURLPathとして認識される()
        {
            Assert.IsType<UrlPath>(PathFactory.Create("https://www.google.com/"));
        }

        [Fact]
        public void http_スタートでなければファイルパス扱いとなる()
        {
            Assert.IsType<FilePath>(PathFactory.Create(@"C:\Directory\File.txt"));
        }
        //public string Path => throw new NotImplementedException();

        //public IPath ParentPath => throw new NotImplementedException();

        //public bool Exists => throw new NotImplementedException();
    }
}
