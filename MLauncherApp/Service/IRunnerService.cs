using LauncherModelLib.Path.Paths;

namespace MLauncherApp.Service
{
    public interface IRunnerService
    {
        public void Run(IPath matchedPath);
    }
}
