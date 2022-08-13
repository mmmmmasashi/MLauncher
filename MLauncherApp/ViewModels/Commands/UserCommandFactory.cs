using LauncherModelLib;
using MLauncherApp.Service;
using MLauncherApp.ViewModels.Commands.Imp;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MLauncherApp.ViewModels.Commands
{
    internal class UserCommandFactory
    {
        private readonly Dictionary<string, IUserCommand> _specialCommandDictionary;

        private IFilePathRepository filePathRepository;
        private IPathCandidateFilter pathCandidateFilter;
        private IDialogService dialogService;
        private IRunnerService runnerService;

        private IUserCommand ShowAll;
        private PathListWindowService pathListWindowService;

        public UserCommandFactory(IFilePathRepository filePathRepository, 
            IPathCandidateFilter pathCandidateFilter, IDialogService dialogService, 
            IRunnerService runnerService, PathListWindowService pathListWindowService)
        {
            this.filePathRepository = filePathRepository;
            this.pathCandidateFilter = pathCandidateFilter;
            this.dialogService = dialogService;
            this.runnerService = runnerService;
            this.pathListWindowService = pathListWindowService;

            ShowAll = new ShowAllCommand(pathListWindowService, filePathRepository);

            _specialCommandDictionary = new Dictionary<string, IUserCommand>()
            {
                { "/all", ShowAll },
            };
        }

        internal IUserCommand Create(string userInput, bool parentCall)
        {
            if (userInput == null) return new DoNothingCommand();
            if (_specialCommandDictionary.ContainsKey(userInput)) return _specialCommandDictionary[userInput];

            var matchedPathList = pathCandidateFilter.Filter(userInput);

            //ヒットなし
            if (matchedPathList.Count == 0)
            {
                if (!File.Exists(userInput)) return new NotFoundDialogCommand(dialogService);
                throw new NotImplementedException();
                //return new RegisterPathCommand(userInput, filePathRepository, dialogService);
            }

            //一つだけヒット
            if (matchedPathList.Count == 1) return new RunCommand(matchedPathList.First(), parentCall, runnerService);

            ////複数ヒット
            if (matchedPathList.Count > 1) return new ShowCandidatesCommand(matchedPathList, pathListWindowService);

            throw new InvalidProgramException("来ないはず");
        }

    }
}
