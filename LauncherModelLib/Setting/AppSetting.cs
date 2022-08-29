using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Setting
{
    public class AppSetting
    {
        public string SettingFilePath { get; }

        public AppSetting(string rootDir)
        {
            this.SettingFilePath = rootDir + @"\path_list.txt";
        }
    }
}
