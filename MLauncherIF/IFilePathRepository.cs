using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherIF
{
    public interface IFilePathRepository
    {
        void Save(FilePath filePath);
        List<FilePath> Load();
        void Delete(FilePath filePath);
        public Action? UpdatedCallBack { get; set; }
    }
}
