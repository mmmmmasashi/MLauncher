using LauncherModelLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LauncherModelLibTest
{
    [TestClass]
    public class FilePathRepositoryTest
    {
        private IFilePathRepository _repository;

        [TestInitialize]
        public void �t�@�C���p�X2�ۑ����ꂽ���|�W�g����p��()
        {
            Directory.Delete(@"TestDir", true);
            Directory.CreateDirectory(@"TestDir");

            _repository = new FilePathRepository(@"TestDir\SavedFile.txt");
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));
            _repository.Save(new FilePath(@"C:\directory\filepath2.txt"));
        }

        [TestMethod]
        public void ���|�W�g���͕ۑ������p�X�����ׂēǂݏo����()
        {
            List<FilePath> all = _repository.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [TestMethod]
        public void ���|�W�g���̓t�@�C���ŉi��������Ă���()
        {
            var anotherRepository = new FilePathRepository(@"TestDir\SavedFile.txt");
            var all = anotherRepository.GetAll();
            Assert.AreEqual(2, all.Count);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.AreEqual(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [TestMethod]
        public void �d�������p�X�͓o�^������������()
        {
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));//�ۑ��ς̃p�X
            List<FilePath> all = _repository.GetAll();
            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public void ������Ŋ��S��v�������ł���_��v�����t�@�C���p�X��Ԃ�()
        {
            FilePath matchedPath = _repository.Search("filepath2");
            Assert.AreEqual(new FilePath(@"C:\directory\filepath2.txt"), matchedPath);
        }
    }
}