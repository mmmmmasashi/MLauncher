﻿using LauncherModelLib;
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
        private IFilePathRepository filePathRepository;
        private IPathCandidateFilter pathCandidateFilter;
        private IDialogService dialogService;
        private IRunnerService runnerService;

        private IUserCommand ShowAll;
        private IPathListWindowService pathListWindowService;
        private IPathJudgeService pathJudgeService;

        public UserCommandFactory(IFilePathRepository filePathRepository, 
            IPathCandidateFilter pathCandidateFilter, IDialogService dialogService, 
            IRunnerService runnerService, IPathListWindowService pathListWindowService,
            IPathJudgeService pathJudgeService)
        {
            this.filePathRepository = filePathRepository;
            this.pathCandidateFilter = pathCandidateFilter;
            this.dialogService = dialogService;
            this.runnerService = runnerService;
            this.pathListWindowService = pathListWindowService;
            this.pathJudgeService = pathJudgeService;
        }

        internal IUserCommand Create(string userInput, bool parentCall)
        {
            if (userInput == null || userInput == "") return new DoNothingCommand();
            if (userInput == "/all") return new ShowAllCommand(pathListWindowService, filePathRepository);

            var matchedPathList = pathCandidateFilter.Filter(userInput);

            //ヒットなし
            if (matchedPathList.Count == 0)
            {
                if (pathJudgeService.Exists(new FilePath(userInput))) 
                    return new RegisterPathCommand(userInput, filePathRepository, dialogService);
                return new NotFoundDialogCommand(dialogService);
            }

            //一つだけヒット
            if (matchedPathList.Count == 1) return new RunCommand(matchedPathList.First(), parentCall, runnerService);

            ////複数ヒット
            if (matchedPathList.Count > 1) return new ShowCandidatesCommand(matchedPathList, pathListWindowService);

            throw new InvalidProgramException("来ないはず");
        }

    }
}
