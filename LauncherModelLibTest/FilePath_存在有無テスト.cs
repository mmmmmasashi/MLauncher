using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LauncherModelLib.Path;

namespace LauncherModelLibTest
{
    public class FilePath_存在有無テスト
    {
        string TestDir = "PathJudgeServiceTestDir";
        string TestFile = @"PathJudgeServiceTestDir\FileName.txt";

        public FilePath_存在有無テスト()
        {
            if (Directory.Exists(TestDir))
            {
                Directory.Delete(TestDir, true);
            }
            
            Directory.CreateDirectory(TestDir);
            File.WriteAllText(TestFile, "some_text");
        }

        [Fact]
        public void ローカルに存在するファイルであればTrueを返す()
        {
            Assert.True(new FilePath(TestFile).Exists);
        }

        [Fact]
        public void ローカルに存在しないファイルであればFalseを返す()
        {
            Assert.False(new FilePath("FileNotExist").Exists);
        }

        [Fact]
        public void ローカルに存在するフォルダであればTrueを返す()
        {
            Assert.True(new FilePath(TestDir).Exists);
        }

        [Fact]
        public void ローカルに存在しないフォルダであればFalseを返す()
        {
            Assert.False(new FilePath("Directory_not_found").Exists);
        }

        [Fact]
        public void ダブルクォーテーションで囲まれている場合はそれを除外して判断する()
        {
            Assert.True(new FilePath("\"PathJudgeServiceTestDir\\FileName.txt\"").Exists);
        }
    }
}
