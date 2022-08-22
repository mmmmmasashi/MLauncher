using LauncherModelLib.Path.Paths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Path.Infra
{
    public interface IPathRepository
    {
        void Save(IPath filePath);
        List<IPath> Load();
        void Delete(IPath filePath);
        public EventHandler UpdateEvent { get; set; }//保持しているファイルパス一覧が更新されたときに、利用者に通知する
    }
}
