using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest
{
    public class PathProvideServiceTest
    {
        [Fact]
        public void ファイルパス候補リストアップの疎通確認_これからリッチにする予定()
        {
            PathProvideService service = new PathProvideService();
            var suggestedCollection = service.GetSuggestions("sample");
            Assert.NotNull(suggestedCollection);
        }
    }
}
