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
        List<FilePath> Load();
        void Delete(FilePath filePath);
        public EventHandler UpdateEvent { get; set; }//保持しているファイルパス一覧が更新されたときに、利用者に通知する
    }
}
