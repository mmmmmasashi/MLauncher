using LauncherModelLib.PathModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class URLPathTest
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
    }
}
