using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public class PathCandidateFilter : IPathCandidateFilter
    {
        readonly string[] Separators = new string[] { " ", "　" };
        readonly private FilePathRepository _repository;
        private IEnumerable<FilePath> _masterCandidates;

        public PathCandidateFilter(FilePathRepository repository)
        {
            this._repository = repository;
            _repository.UpdateEvent += ReloadCandidates;
            _masterCandidates = _repository.Load();
        }

        /// <summary>
        /// フィルター文字列で部分一致で絞り込む。
        /// 
        /// 詳細仕様↓
        /// ・半角スペースで分割してAND検索ができる
        /// </summary>
        public List<FilePath> Filter(string query)
        {
            IEnumerable<FilePath> candidates = new List<FilePath>(_masterCandidates);
            var keywords = query.Split(Separators, StringSplitOptions.None);

            foreach (var keyword in keywords)
            {
                candidates = candidates.Where(candidate => candidate.Contains(keyword));
            }
            return candidates.ToList();
        }

        private void ReloadCandidates(object? sender, EventArgs e)
        {
            _masterCandidates = _repository.Load();
        }

    }
}
