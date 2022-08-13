using LauncherModelLib;
using MLauncherApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.ViewModels.Commands.Imp
{
    internal class ShowCandidatesCommand : IUserCommand
    {
        private List<FilePath> _matchedPathList;
        private IPathListWindowService _pathListWindowService;

        public ShowCandidatesCommand(List<FilePath> matchedPathList, IPathListWindowService pathListWindowService)
        {
            this._matchedPathList = matchedPathList;
            this._pathListWindowService = pathListWindowService;
        }

        void IUserCommand.Execute()
        {
            _pathListWindowService.ShowDialog(_matchedPathList);
        }
    }
}
