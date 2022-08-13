using LauncherModelLib;
using MLauncherIF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class PathProvideServiceTest
    {
        IPathSuggestionService _service;

        public PathProvideServiceTest()
        {
            if (Directory.Exists("TestDir"))
            {
                Directory.Delete(@"TestDir", true);
            }
            Directory.CreateDirectory(@"TestDir");

            var repository = new FilePathRepository(@"TestDir\SavedFile.txt");
            repository.Save(new FilePath(@"C:\directory\filepath1.txt"));
            repository.Save(new FilePath(@"C:\directory\filepath2.txt"));
            repository.Save(new FilePath(@"C:\directory\filepath3.txt"));
            repository.Save(new FilePath(@"C:\directory2\filepath1.txt"));
            repository.Save(new FilePath(@"C:\directory2\filepath2.txt"));

            _service = new PathProvideService(repository);
        }

        [Fact]
        public void 文字列で完全一致検索ができて_一致したファイルパスを返す()
        {
            var matchedPath = _service.GetPathSuggestions("filepath3");

            Assert.Single(matchedPath);
            Assert.Equal(new FilePath(@"C:\directory\filepath3.txt"), matchedPath.First());
        }

        [Fact]
        public void 半角スペースでAND検索ができる()
        {
            var matchedPath = _service.GetPathSuggestions("directory2 filepath1");
            Assert.Single(matchedPath);
            Assert.Equal(new FilePath(@"C:\directory2\filepath1.txt"), matchedPath.First());
        }

        [Fact]
        public void 存在しない場合は空リスト()
        {
            var result = _service.GetPathSuggestions("not_existing_text");
            Assert.Empty(result);
        }
    }
}
