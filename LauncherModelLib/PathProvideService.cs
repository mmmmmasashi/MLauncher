using MLauncherIF;
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
        readonly private FilePathRepository _repository;

        public PathProvideService(FilePathRepository repository)
        {
            this._repository = repository;
        }

        public List<FilePath> GetPathSuggestions(string filter)
        {
            List<FilePath> candidates = _repository.Load();
            return candidates.Where(candidate => candidate.Contains(filter)).ToList();
        }

        public IEnumerable GetSuggestions(string filter)
        {
            return GetPathSuggestions(filter);
        }
    }
}
