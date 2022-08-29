using MLauncherApp.Setting;
using System;
using Xunit;

namespace MLauncherAppTest
{
    public class SettingRepositoryTest
    {
        [Fact]
        public void LoadするとAppSettingが取れる()
        {
            MLauncherApp.Properties.Settings settings = new MLauncherApp.Properties.Settings();
            settings.IsInitialized = false;
            settings.SettingFilePath = "";

            ISettingRepository repository = new SettingRepository(settings);
            AppSetting setting = repository.Load();
            Assert.True(setting.SettingFilePath.EndsWith("path_list.txt"));
        }

    }
    
    public class SettingRepository_ファイルパスの保存先パスが設定済_Test
    {
        private ISettingRepository repository;

        public SettingRepository_ファイルパスの保存先パスが設定済_Test()
        {
            MLauncherApp.Properties.Settings _settings = new MLauncherApp.Properties.Settings();
            _settings.IsInitialized = true;
            _settings.SettingFilePath = "saved_path.txt";
            repository = new SettingRepository(_settings);
        }

        [Fact]
        public void 保存したパスが読める()
        {
            var savedSetting = repository.Load();
            Assert.Equal("saved_path.txt", savedSetting.SettingFilePath);
        }

        [Fact]
        public void アプリ設定をSaveすると上書きできる()
        {
            repository.Save(new AppSetting("new_path.txt"));

            var savedSetting = repository.Load();
            Assert.Equal("new_path.txt", savedSetting.SettingFilePath);
        }
    }
}
