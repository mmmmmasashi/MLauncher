﻿using LauncherModelLib;
using MLauncherIF;
using System;
using Xunit;

namespace LauncherModelLibTest
{

    public class FilePathRepositoryTest
    {
        IFilePathRepository _repository;

        public FilePathRepositoryTest()
        {
            if (Directory.Exists(@"TestForFilePathRepo"))
            {
                Directory.Delete(@"TestForFilePathRepo", true);
            }
            Directory.CreateDirectory(@"TestForFilePathRepo");

            _repository = new FilePathRepository(@"TestForFilePathRepo\RepositoryTest.txt");
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));
            _repository.Save(new FilePath(@"C:\directory\filepath2.txt"));
        }

        [Fact]
        public void Loadで保存していたデータ全てをリストアップ()
        {
            Assert.Equal(2, _repository.Load().Count);
        }


        [Fact]
        public void リポジトリはファイルで永続化されている()
        {
            var anotherRepository = new FilePathRepository(@"TestForFilePathRepo\RepositoryTest.txt");
            var all = anotherRepository.Load();
            Assert.Equal(2, all.Count);
            Assert.Equal(new FilePath(@"C:\directory\filepath1.txt"), all[0]);
            Assert.Equal(new FilePath(@"C:\directory\filepath2.txt"), all[1]);
        }

        [Fact]
        public void 新規のパスを登録すると登録される()
        {
            _repository.Save(new FilePath(@"C:\directory\filepath3.txt"));
            Assert.Equal(3, _repository.Load().Count);
            Assert.Equal(new FilePath(@"C:\directory\filepath3.txt"), _repository.Load()[2]);
        }

        [Fact]
        public void 重複したパスは登録せず無視する()
        {
            _repository.Save(new FilePath(@"C:\directory\filepath1.txt"));//保存済のパス
            Assert.Equal(2, _repository.Load().Count);
        }

    }
}