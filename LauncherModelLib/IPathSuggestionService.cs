using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherModelLib
{
    public interface IPathSuggestionService
    {
        List<FilePath> GetPathSuggestions(string filter);
    }
}
