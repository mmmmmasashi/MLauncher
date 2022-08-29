namespace MLauncherApp.Setting
{
    public interface ISettingRepository
    {
        AppSetting Load();
        void Save(AppSetting appSetting);
    }
}
