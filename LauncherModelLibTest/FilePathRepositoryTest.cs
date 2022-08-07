using LauncherModelLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LauncherModelLibTest
{
    [TestClass]
    public class FilePathRepositoryTest
    {
        private IFilePathRepository _repository;

        [TestInitialize]
        public void ファイルパス2つ保存されたリポジトリを用意()
        {
            Directory.Delete(@"TestDir", true);
            Directory.CreateDirectory(@"TestDir");

            _repository = new FilePathRepository(@"TestDir\SavedFile.txt");
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));
            _repository.Save(new FilePath(@"C:\directory\filepath2.txt"));
        }

        [TestMethod]
        public void リポジトリは保存したパスをすべて読み出せる()
        {
            List<FilePath> all = _repository.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [TestMethod]
        public void リポジトリはファイルで永続化されている()
        {
            var anotherRepository = new FilePathRepository(@"TestDir\SavedFile.txt");
            var all = anotherRepository.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [TestMethod]
        public void 重複したパスは登録せず無視する()
        {
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));//保存済のパス
            List<FilePath> all = _repository.GetAll();
            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public void 文字列で完全一致検索ができて_一致したファイルパスを返す()
        {
            FilePath matchedPath = _repository.Search("filepath2");
            Assert.AreEqual(new FilePath(@"C:\directory\filepath2.txt"), matchedPath);
        }
    }
}