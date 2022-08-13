using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands
{
    /// <summary>
    /// ユーザーの入力にしたがって行われるコマンド
    /// </summary>
    internal interface IUserCommand
    {
        internal void Execute();
    }
}
