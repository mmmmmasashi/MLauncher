using LauncherModelLib.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LauncherModelLibTest.Setting
{
    public class SettingRepositoryTest
    {
        [Fact]
        public void LoadするとAppSettingが取れる()
        {
            ISettingRepository repository = new SettingRepository(@"C:\RootDir");
            AppSetting setting = repository.Load();
            Assert.Equal(@"C:\RootDir\path_list.txt", setting.SettingFilePath);
        }
    }
}
