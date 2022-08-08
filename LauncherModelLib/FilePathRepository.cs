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

        public FilePath FilePath => new FilePath(_savedFilePath);

        public FilePathRepository(string savedFilePath)
        {
            this._savedFilePath = savedFilePath;
        }

        private List<FilePath> GetAll()
        {
            if (!File.Exists(_savedFilePath)) return new List<FilePath>();
            var lines = File.ReadAllLines(_savedFilePath);
            return lines.Select(line => new FilePath(line)).ToList();
        }

        public void Save(FilePath filePath)
        {
            var all = GetAll();
            if (all.Contains(filePath)) return;
            File.AppendAllText(_savedFilePath, filePath.Path + "\r\n");
        }

        /// <summary>
        /// 検索文字列を含むパスを返す。
        /// </summary>
        public List<FilePath> Search(string text)
        {
            var all = GetAll();
            return all.Where(filePath => filePath.Contains(text)).ToList();
        }
    }
}
