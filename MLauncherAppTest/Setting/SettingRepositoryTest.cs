using MLauncherApp.Setting;

namespace MLauncherAppTest
{
    public class SettingRepositoryTest
    {
        [Fact]
        public void LoadするとAppSettingが取れる()
        {
            ISettingRepository repository = new SettingRepository();
            AppSetting setting = repository.Load();
            Assert.True(setting.SettingFilePath.EndsWith("path_list.txt"));
        }
    }
}
