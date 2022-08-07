using LauncherModelLib;
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
            List<FilePath> all = _repository.GetAll();
            Assert.Equal(2, all.Count);
            Assert.Equal(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [Fact]
        public void リポジトリはファイルで永続化されている()
        {
            var anotherRepository = new FilePathRepository(@"TestDir\SavedFile.txt");
            var all = anotherRepository.GetAll();
            Assert.Equal(2, all.Count);
            Assert.Equal(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [Fact]
        public void 重複したパスは登録せず無視する()
        {
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));//保存済のパス
            List<FilePath> all = _repository.GetAll();
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public void 文字列で完全一致検索ができて_一致したファイルパスを返す()
        {
            FilePath matchedPath = _repository.Search("filepath2");
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), matchedPath);
        }

        [Fact]
        public void 存在しない場合は例外()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _repository.Search("not_existing_text");
            });
        }
    }
}