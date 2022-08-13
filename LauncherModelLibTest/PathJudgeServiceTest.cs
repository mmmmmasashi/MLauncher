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
            Assert.True(judge.Exists(TestFile));
        }

        [Fact]
        public void ローカルに存在しないファイルであればFalseを返す()
        {
            Assert.False(judge.Exists("FileNotExist"));
        }

        [Fact]
        public void ローカルに存在するフォルダであればTrueを返す()
        {
            Assert.True(judge.Exists(TestDir));
        }

        [Fact]
        public void ローカルに存在しないフォルダであればFalseを返す()
        {
            Assert.False(judge.Exists("Directory_not_found"));
        }

    }
}
