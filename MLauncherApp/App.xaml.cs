using AutoCompleteTextBox.Editors;
using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.Views;
using Prism.Ioc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            const string RegisteredPathTextFile = "path_list.txt";//TODO:可変にする。今は固定でexeの隣に保存している
            var repository = new FilePathRepository(RegisteredPathTextFile);
            containerRegistry.RegisterInstance<IFilePathRepository>(repository);
            containerRegistry.RegisterInstance<IPathCandidateFilter>(new PathCandidateFilter(repository));
            containerRegistry.RegisterSingleton<IRunnerService, RunnerService>();
            containerRegistry.RegisterSingleton<IPathJudgeService, PathJudgeService>();

            containerRegistry.RegisterDialog<PathListControl>();

            containerRegistry.RegisterDialogWindow<DialogWindow>();

        }
    }
}
