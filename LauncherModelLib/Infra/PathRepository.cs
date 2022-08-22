using LauncherModelLib.PathModel;
using System.Text;

namespace LauncherModelLib.Infra
{
    public class PathRepository : IPathRepository
    {
        private string _savedFilePath;
        private List<IPath> _filePathList = new List<IPath>();

        public EventHandler UpdateEvent { get; set; }//保持しているファイルパス一覧が更新されたときに、利用者に通知する

        public PathRepository(string savedFilePath)
        {
            _savedFilePath = savedFilePath;
            _filePathList = Load();
        }

        public void Save(IPath filePath)
        {
            if (_filePathList.Contains(filePath)) return;

            _filePathList.Add(filePath);
            SaveAllImp();

            NotifyUpdated();
        }


        public List<IPath> Load()
        {
            if (!File.Exists(_savedFilePath)) return new List<IPath>();
            var lines = File.ReadAllLines(_savedFilePath);
            return lines.Select(line => PathFactory.Create(line)).ToList<IPath>();
        }

        public void Delete(IPath filePath)
        {
            _filePathList.Remove(filePath);
            SaveAllImp();
            NotifyUpdated();
        }

        private void SaveAllImp()
        {
            File.WriteAllLines(_savedFilePath, _filePathList.Select(filePath => filePath.Path), Encoding.UTF8);
        }

        private void NotifyUpdated()
        {
            if (UpdateEvent != null)
            {
                UpdateEvent(this, new EventArgs());
            }
        }
    }
}
