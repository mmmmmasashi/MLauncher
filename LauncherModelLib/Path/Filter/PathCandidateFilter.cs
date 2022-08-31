using LauncherModelLib.Path.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherModelLib.Path.Paths;
using KaoriYa.Migemo;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LauncherModelLib.Path.Filter
{
    public class PathCandidateFilter : IPathCandidateFilter
    {
        readonly string[] Separators = new string[] { " ", "　" };
        readonly private PathRepository _repository;
        private IEnumerable<IPath> _masterCandidates;
        private readonly Migemo _migemo;

        public PathCandidateFilter(PathRepository repository)
        {
            _repository = repository;
            _repository.UpdateEvent += ReloadCandidates;
            _masterCandidates = _repository.Load();

            _migemo = new Migemo("./Migemo/dict/cp932/migemo-dict");
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

            var regexList = keywords.Select(keyword => _migemo.GetRegex(keyword));
            //Trace.WriteLine(regex.ToString());

            foreach (var regex in regexList)
            {
                candidates = candidates.Where(candidate => regex.IsMatch(candidate.PathToRead));
            }

            return candidates.ToList();
        }

        private void ReloadCandidates(object? sender, EventArgs e)
        {
            _masterCandidates = _repository.Load();
        }

    }
}
