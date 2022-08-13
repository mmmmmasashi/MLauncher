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

        public PathJudgeServiceTest()
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
            var judge = new PathJudgeService();
            Assert.True(judge.Exists(TestFile));
        }

        [Fact]
        public void ローカルに存在しないファイルであればFalseを返す()
        {
            var judge = new PathJudgeService();
            Assert.False(judge.Exists("FileNotExist"));
        }

        [Fact (Skip ="他のテストが通ってから")]
        public void ローカルに存在するフォルダであればTrueを返す()
        {
            throw new NotImplementedException();
        }

    }
}
