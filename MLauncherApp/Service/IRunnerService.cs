using LauncherModelLib.PathModel;

namespace MLauncherApp.Service
{
    public interface IRunnerService
    {
        public void Run(IPath matchedPath);
    }
}
