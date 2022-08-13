using AutoCompleteTextBox.Editors;
using LauncherModelLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    internal class PathSuggestionProviderForView : ISuggestionProvider
    {
        private IPathSuggestionService pathSuggestionProvider;

        public PathSuggestionProviderForView(IPathSuggestionService pathSuggestionProvider)
        {
            this.pathSuggestionProvider = pathSuggestionProvider;
        }

        public IEnumerable GetSuggestions(string filter)
        {
            return pathSuggestionProvider.GetPathSuggestions(filter).Select(path => path.Path);
        }
    }
}
