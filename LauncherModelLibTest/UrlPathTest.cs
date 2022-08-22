using LauncherModelLib.PathModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class UrlPathTest
    {
        private IPath _path;

        public UrlPathTest()
        {
            _path = new UrlPath("https://www.google.com/");
        }

        [Fact]
        public void Pathはそのまま()
        {
            Assert.Equal("https://www.google.com/", _path.Path);
        }

        [Fact]
        public void ParentPathはないので自分自身を返す_Ctrlを押しながら起動しても変化なしということ()
        {
            Assert.Equal(new UrlPath("https://www.google.com/"), _path.ParentPath);
        }

        [Fact]
        public void 存在有無はチェックできないので常時True()
        {
            Assert.True(_path.Exists);
        }
    }
}
