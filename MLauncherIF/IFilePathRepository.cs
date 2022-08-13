using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherIF
{
    public interface IFilePathRepository
    {
        FilePath ListCommandFile { get; }
        void Save(FilePath filePath);
        List<FilePath> Load();
    }
}
