using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class PathProvideService : IPathSuggestionService
    {
        readonly string[] Separators = new string[] { " ", "　" };
        readonly private FilePathRepository _repository;
        private IEnumerable<FilePath> _masterCandidates;

        public PathProvideService(FilePathRepository repository)
        {
            this._repository = repository;
            _repository.UpdateEvent += ReloadCandidates;
            _masterCandidates = _repository.Load();
        }

        private void ReloadCandidates(object? sender, EventArgs e)
        {
            ReloadCandidates();
        }

        /// <summary>
        /// フィルター文字列で部分一致で絞り込む。
        /// 
        /// 詳細仕様↓
        /// ・半角スペースで分割してAND検索ができる
        /// </summary>
        public List<FilePath> GetPathSuggestions(string filter)
        {
            IEnumerable<FilePath> candidates = new List<FilePath>(_masterCandidates);
            var keywords = filter.Split(Separators, StringSplitOptions.None);

            foreach (var keyword in keywords)
            {
                candidates = candidates.Where(candidate => candidate.Contains(keyword));
            }
            return candidates.ToList();
        }

        private void ReloadCandidates()
        {
            _masterCandidates = _repository.Load();
        }
    }
}
