using LauncherModelLib;
using MLauncherIF;
using System;
using Xunit;

namespace LauncherModelLibTest
{

    public class FilePathRepositoryTest
    {
        private IFilePathRepository _repository;

        public FilePathRepositoryTest()
        {
            if (Directory.Exists("TestDir"))
            {
                Directory.Delete(@"TestDir", true);
            }
            Directory.CreateDirectory(@"TestDir");

            _repository = new FilePathRepository(@"TestDir\SavedFile.txt");
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));
            _repository.Save(new FilePath(@"C:\directory\filepath2.txt"));
        }

        [Fact]
        public void リポジトリは保存したパスをすべて読み出せる()
        {
            List<FilePath> all = _repository.Search("C");
            Assert.Equal(2, all.Count);
            Assert.Equal(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [Fact]
        public void リポジトリはファイルで永続化されている()
        {
            var anotherRepository = new FilePathRepository(@"TestDir\SavedFile.txt");
            var all = anotherRepository.Search("C");
            Assert.Equal(2, all.Count);
            Assert.Equal(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [Fact]
        public void 重複したパスは登録せず無視する()
        {
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));//保存済のパス
            List<FilePath> all = _repository.Search("C");
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public void 文字列で完全一致検索ができて_一致したファイルパスを返す()
        {
            List<FilePath> matchedPath = _repository.Search("filepath2");

            Assert.Single(matchedPath);
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), matchedPath.First());
        }

        [Fact]
        public void 存在しない場合は空リスト()
        {
            var result = _repository.Search("not_existing_text");
            Assert.Empty(result);
        }
    }
}