using LauncherModelLib.Path.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherModelLib.Path.Paths;

namespace LauncherModelLib.Path.Filter
{
    public class PathCandidateFilter : IPathCandidateFilter
    {
        readonly string[] Separators = new string[] { " ", "　" };
        readonly private PathRepository _repository;
        private IEnumerable<IPath> _masterCandidates;

        public PathCandidateFilter(PathRepository repository)
        {
            _repository = repository;
            _repository.UpdateEvent += ReloadCandidates;
            _masterCandidates = _repository.Load();
        }

        /// <summary>
        /// 文字列で絞り込む。
        /// 
        /// 詳細仕様↓
        /// ・半角スペースで分割してAND検索ができる
        /// </summary>
        public List<IPath> Filter(string query)
        {
            IEnumerable<IPath> candidates = new List<IPath>(_masterCandidates);
            var keywords = query.Split(Separators, StringSplitOptions.None);

            foreach (var keyword in keywords)
            {
                candidates = candidates.Where(candidate => candidate.Path.Contains(keyword));
            }
            return candidates.ToList();
        }

        private void ReloadCandidates(object? sender, EventArgs e)
        {
            _masterCandidates = _repository.Load();
        }

    }
}
