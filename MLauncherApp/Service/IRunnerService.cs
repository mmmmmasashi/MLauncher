using LauncherModelLib.Path;

namespace MLauncherApp.Service
{
    public interface IRunnerService
    {
        public void Run(FilePath matchedPath);
    }
}
