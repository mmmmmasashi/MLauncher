using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public interface IFilePathRepository
    {
        void Save(FilePath filePath);
        List<FilePath> GetAll();
        FilePath Search(string text);
        bool AnyHit(string userInput);
    }
}
