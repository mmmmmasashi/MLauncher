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

        public FilePathRepository(string savedFilePath)
        {
            this._savedFilePath = savedFilePath;
        }

        public List<FilePath> GetAll()
        {
            var lines = File.ReadAllLines(_savedFilePath);
            return lines.Select(line => new FilePath(line)).ToList();
        }

        public void Save(FilePath filePath)
        {
            File.AppendAllText(_savedFilePath, filePath.Path + "\r\n");
        }
    }
}
