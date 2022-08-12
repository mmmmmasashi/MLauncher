using AutoCompleteTextBox.Editors;
using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.Views;
using MLauncherIF;
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
            const string RegisteredPathTextFile = "path_list.txt";//TODO:可変にする。今は固定でexeの隣に保存している
            containerRegistry.RegisterInstance<IFilePathRepository>(new FilePathRepository(RegisteredPathTextFile));
            containerRegistry.Register<IPathSuggestionService>((() => new PathProvideService()));
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<IRunnerService, RunnerService>();

            containerRegistry.RegisterDialog<PathListControl>();

        }
    }
}
