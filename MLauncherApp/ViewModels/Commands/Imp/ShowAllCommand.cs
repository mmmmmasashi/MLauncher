using LauncherModelLib.Infra;
using MLauncherApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class ShowAllCommand : IUserCommand
    {
        private IPathListWindowService _pathListWindowService;
        private IPathRepository _repository;

        public ShowAllCommand(IPathListWindowService pathListWindowService, IPathRepository filePathRepository)
        {
            this._pathListWindowService = pathListWindowService;
            this._repository = filePathRepository;
        }

        void IUserCommand.Execute()
        {
            _pathListWindowService.ShowDialog(_repository.Load(), "全登録済パスリスト");
        }
    }
}
