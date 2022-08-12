using LauncherModelLib;
using MLauncherIF;
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
        IPathSuggestionService _service;

        public PathProvideServiceTest()
        {
            _service = new PathProvideService();
        }

        [Fact]
        public void ファイルパス候補リストアップの疎通確認_これからリッチにする予定()
        {
            var suggestedCollection = _service.GetSuggestions("sample");
            Assert.NotNull(suggestedCollection);
        }

    }
}
