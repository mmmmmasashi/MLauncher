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


        public override int GetHashCode()
        {
            return HashCode.Combine(Path);
        }

        public bool Contains(string text)
        {
            return Path.Contains(text);
        }
    }
}