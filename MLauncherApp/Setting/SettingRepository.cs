namespace MLauncherApp.Setting
{
    internal class SettingRepository : ISettingRepository
    {
        internal SettingRepository(string rootDir)
        {
        }

        public AppSetting Load()
        {
            return new AppSetting();
        }
    }
}
