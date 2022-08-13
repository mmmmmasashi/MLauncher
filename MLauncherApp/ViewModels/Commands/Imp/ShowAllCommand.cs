using LauncherModelLib;
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
        private PathListWindowService _pathListWindowService;
        private IFilePathRepository _repository;

        public ShowAllCommand(PathListWindowService pathListWindowService, LauncherModelLib.IFilePathRepository filePathRepository)
        {
            this._pathListWindowService = pathListWindowService;
            this._repository = filePathRepository;
        }

        void IUserCommand.Execute()
        {
            _pathListWindowService.ShowDialog(_repository.Load());
        }
    }
}
