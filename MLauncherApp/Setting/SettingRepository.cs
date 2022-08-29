namespace MLauncherApp.Setting
{
    internal class SettingRepository : ISettingRepository
    {
        internal SettingRepository()
        {
        }

        public AppSetting Load()
        {
            string pathListPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\path_list.txt";
            return new AppSetting(pathListPath);
        }
    }
}
