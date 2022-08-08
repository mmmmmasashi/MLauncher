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

        public List<FilePath> GetAll()
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
        /// 完全一致するパスが一つでもあるか
        /// </summary>
        public bool AnyHit(string userInput)
        {
            return (GetAll().Any(path => path.Contains(userInput)));
        }

        /// <summary>
        /// 検索文字列を含むパスを返す。
        /// 見つからない場合は例外
        /// </summary>
        public FilePath Search(string text)
        {
            var all = GetAll();
            return all.First(filePath => filePath.Contains(text));
        }
    }
}
