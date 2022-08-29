using MLauncherApp.Properties;
using System;

namespace MLauncherApp.Setting
{
    internal class SettingRepository : ISettingRepository
    {
        private readonly Settings _settings;

        internal SettingRepository(Settings settings = null)
        {
            _settings = (settings == null)? Settings.Default : settings;
        }

        public AppSetting Load()
        {
            if (_settings.IsInitialized)
            {
                return LoadExistingSetting();
            }
            else
            {
                return CreateDefaultSetting();
            }
        }

        private AppSetting CreateDefaultSetting()
        {
            string pathListPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\path_list.txt";
            return new AppSetting(pathListPath);
        }

        private AppSetting LoadExistingSetting()
        {
            string pathListPath = _settings.SettingFilePath;
            return new AppSetting(pathListPath);
        }

        public void Save(AppSetting appSetting)
        {
            _settings.SettingFilePath = appSetting.SettingFilePath;
            _settings.IsInitialized = true;
            _settings.Save();
        }
    }
}
