using LauncherModelLib;

namespace MLauncherApp.Service
{
    public interface IRunnerService
    {
        public void Run(FilePath matchedPath);
    }
}
