using AutoCompleteTextBox.Editors;
using LauncherModelLib.Path.Filter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal class AutoCompleteProvider : ISuggestionProvider
    {
        private IPathCandidateFilter pathSuggestionProvider;

        public AutoCompleteProvider(IPathCandidateFilter pathSuggestionProvider)
        {
            this.pathSuggestionProvider = pathSuggestionProvider;
        }

        public IEnumerable GetSuggestions(string filter)
        {
            return pathSuggestionProvider.Filter(filter).Select(path => path.PathToRead);
        }
    }
}
