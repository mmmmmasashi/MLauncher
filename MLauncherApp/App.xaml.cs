using LauncherModelLib.Path.Existence;
using LauncherModelLib.Path.Filter;
using LauncherModelLib.Path.Infra;
using MLauncherApp.Service;
using MLauncherApp.Setting;
using MLauncherApp.Views;
using Prism.Ioc;
using System.Windows;

namespace MLauncherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageControl>();
            containerRegistry.RegisterDialog<ConfirmControl>();
            containerRegistry.RegisterDialog<SettingControl>();

            var settingRespository = new SettingRepository();
            containerRegistry.RegisterInstance<ISettingRepository>(settingRespository);

            var setting = settingRespository.Load();
            string registeredPathTextFile = setting.SettingFilePath;
            var repository = new PathRepository(registeredPathTextFile);
            containerRegistry.RegisterInstance<IPathRepository>(repository);
            containerRegistry.RegisterInstance<IPathCandidateFilter>(new PathCandidateFilter(repository));
            containerRegistry.RegisterSingleton<IRunnerService, RunnerService>();
            containerRegistry.RegisterSingleton<IPathJudgeService, PathJudgeService>();

            containerRegistry.RegisterDialog<PathListControl>();

            containerRegistry.RegisterDialogWindow<DialogWindow>();

        }
    }
}
