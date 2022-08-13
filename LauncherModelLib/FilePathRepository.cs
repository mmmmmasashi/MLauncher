using MLauncherIF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class FilePathRepository : IFilePathRepository
    {
        private string _savedFilePath;
        private List<FilePath> _filePathList = new List<FilePath>();

        public Action? UpdatedCallBack { get; internal set; }//保持しているファイルパス一覧が更新されたときに、利用者に通知する

        public FilePathRepository(string savedFilePath)
        {
            this._savedFilePath = savedFilePath;
            _filePathList = Load();
        }

        public void Save(FilePath filePath)
        {
            if (_filePathList.Contains(filePath)) return;
            
            _filePathList.Add(filePath);
            File.AppendAllText(_savedFilePath, filePath.Path + "\r\n");

            if(UpdatedCallBack != null)
            {
                UpdatedCallBack();
            }
        }

        public List<FilePath> Load()
        {
            if (!File.Exists(_savedFilePath)) return new List<FilePath>();
            var lines = File.ReadAllLines(_savedFilePath);
            return lines.Select(line => new FilePath(line)).ToList();
        }
    }
}
