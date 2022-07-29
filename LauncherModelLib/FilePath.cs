namespace LauncherModelLib
{
    public class FilePath
    {
        public string Path { get; }

        public FilePath(string path)
        {
            this.Path = path;
        }

        public override bool Equals(object? obj)
        {
            return obj is FilePath path &&
                   Path == path.Path;
        }
    }
}