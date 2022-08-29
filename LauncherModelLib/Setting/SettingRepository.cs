using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib.Setting
{
    internal class SettingRepository : ISettingRepository
    {
        private readonly string _rootDir;

        //TODO:Factoryにしてinternalにする
        public SettingRepository(string rootDir)
        {
            _rootDir = rootDir;
        }

        public AppSetting Load()
        {
            return new AppSetting(_rootDir);
        }
    }
}
