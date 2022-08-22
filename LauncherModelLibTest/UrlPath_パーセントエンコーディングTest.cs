using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class UrlPath_パーセントエンコーディングTest
    {

        private IPath _path;

        public UrlPath_パーセントエンコーディングTest()
        {
            _path = new UrlPath("https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0");

            //デコードすると以下の文字列になる
            //https://ja.wikipedia.org/wiki/パーセントエンコーディング
        }

        [Fact]
        public void Pathはそのまま()
        {
            Assert.Equal(
                "https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0",
                _path.Path);
        }

        [Fact]
        public void PathToReadはデコードしたものを返す()
        {
            Assert.Equal(
                "https://ja.wikipedia.org/wiki/パーセントエンコーディング",
                _path.PathToRead
            );
        }

        [Fact]
        public void ParentPathはないので自分自身を返す_Ctrlを押しながら起動しても変化なしということ()
        {
            Assert.Equal(
                new UrlPath("https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0"),
                _path.ParentPath
                );
        }

        [Fact]
        public void 存在有無はチェックできないので常時True()
        {
            Assert.True(_path.Exists);
        }
    }
}
