namespace MLauncherApp.Setting
{
    public class AppSetting
    {
        public string SettingFilePath { get; }

        public AppSetting()
        {
            this.SettingFilePath = @"C:\RootDir" + @"\path_list.txt";
        }
    }
}
