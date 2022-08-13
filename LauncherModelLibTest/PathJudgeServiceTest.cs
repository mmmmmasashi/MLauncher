using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class PathJudgeServiceTest
    {
        string TestDir = "PathJudgeServiceTestDir";
        string TestFile = @"PathJudgeServiceTestDir\FileName.txt";
        PathJudgeService judge;

        public PathJudgeServiceTest()
        {
            if (Directory.Exists(TestDir))
            {
                Directory.Delete(TestDir, true);
            }
            
            Directory.CreateDirectory(TestDir);
            File.WriteAllText(TestFile, "some_text");

            judge = new PathJudgeService();

        }

        [Fact]
        public void ローカルに存在するファイルであればTrueを返す()
        {
            Assert.True(judge.Exists(new FilePath(TestFile)));
        }

        [Fact]
        public void ローカルに存在しないファイルであればFalseを返す()
        {
            Assert.False(judge.Exists(new FilePath("FileNotExist")));
        }

        [Fact]
        public void ローカルに存在するフォルダであればTrueを返す()
        {
            Assert.True(judge.Exists(new FilePath(TestDir)));
        }

        [Fact]
        public void ローカルに存在しないフォルダであればFalseを返す()
        {
            Assert.False(judge.Exists(new FilePath("Directory_not_found")));
        }

        [Fact]
        public void ダブルクォーテーションで囲まれている場合はそれを除外して判断する()
        {
            Assert.True(judge.Exists(new FilePath("\"PathJudgeServiceTestDir\\FileName.txt\"")));
        }
    }
}
