namespace MLauncherApp.Setting
{
    public class AppSetting
    {
        private string pathListPath;

        public string SettingFilePath { get => pathListPath; }

        public AppSetting(string pathListPath)
        {
            this.pathListPath = pathListPath;
        }
    }
}
