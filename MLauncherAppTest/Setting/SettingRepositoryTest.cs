using MLauncherApp.Setting;

namespace MLauncherAppTest
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
