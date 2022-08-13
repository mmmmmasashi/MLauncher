﻿using MLauncherIF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class PathProvideService : IPathSuggestionService
    {
        readonly string[] Separators = new string[] { " ", "　"};
        readonly private FilePathRepository _repository;
        private IEnumerable<FilePath> candidates;

        public PathProvideService(FilePathRepository repository)
        {
            this._repository = repository;
            _repository.UpdatedCallBack += LoadCandidates;
            LoadCandidates();
        }

        /// <summary>
        /// フィルター文字列で部分一致で絞り込む。
        /// 
        /// 詳細仕様↓
        /// ・半角スペースで分割してAND検索ができる
        /// </summary>
        public List<FilePath> GetPathSuggestions(string filter)
        {
            var keywords = filter.Split(Separators, StringSplitOptions.None);
            
            foreach (var keyword in keywords)
            {
                candidates = candidates.Where(candidate => candidate.Contains(keyword));
            }
            return candidates.ToList();
        }

        public IEnumerable GetSuggestions(string filter)
        {
            List<FilePath> filePathList = GetPathSuggestions(filter);
            return filePathList.Select(path => path.Path);
        }

        private void LoadCandidates()
        {
            candidates = _repository.Load();
        }
    }
}