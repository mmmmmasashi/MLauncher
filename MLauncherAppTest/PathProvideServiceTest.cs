using LauncherModelLib;
using MLauncherApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherModelLib.Path.Paths;
using LauncherModelLib.Path.Infra;
using LauncherModelLib.Path.Filter;

namespace MLauncherAppTest
{
    public class PathProvideServiceTest
    {
        IPathCandidateFilter _service;

        public PathProvideServiceTest()
        {
            if (Directory.Exists("TestDir"))
            {
                Directory.Delete(@"TestDir", true);
            }
            Directory.CreateDirectory(@"TestDir");

            var repository = new PathRepository(@"TestDir\SavedFile.txt");
            repository.Save(new FilePath(@"C:\directory\filepath1.txt"));
            repository.Save(new FilePath(@"C:\directory\filepath2.txt"));
            repository.Save(new FilePath(@"C:\directory\filepath3.txt"));
            repository.Save(new FilePath(@"C:\directory2\filepath1.txt"));
            repository.Save(new FilePath(@"C:\directory2\filepath2.txt"));

            _service = new PathCandidateFilter(repository);
        }

        [Fact]
        public void 文字列で完全一致検索ができて_一致したファイルパスを返す()
        {
            var matchedPath = _service.Filter("filepath3");

            Assert.Single(matchedPath);
            Assert.Equal(new FilePath(@"C:\directory\filepath3.txt"), matchedPath.First());
        }

        [Fact]
        public void 半角スペースで区切ってAND検索ができる()
        {
            var matchedPath = _service.Filter("directory2 filepath1");
            Assert.Single(matchedPath);
            Assert.Equal(new FilePath(@"C:\directory2\filepath1.txt"), matchedPath.First());
        }

        [Fact]
        public void 全角スペースで区切ってAND検索ができる()
        {
            var matchedPath = _service.Filter("directory2　filepath1");
            Assert.Single(matchedPath);
            Assert.Equal(new FilePath(@"C:\directory2\filepath1.txt"), matchedPath.First());
        }

        [Fact]
        public void 存在しない場合は空リスト()
        {
            var result = _service.Filter("not_existing_text");
            Assert.Empty(result);
        }

        [Fact]
        public void 不具合検査_検索するとヒットした候補だけに候補が減ってしまう問題()
        {
            //一度検索すると
            var matchedPath = _service.Filter("filepath1");

            //それ以外の候補がヒットしなくなっていた
            Assert.NotEmpty(_service.Filter("filepath2"));
        }
    }
}
