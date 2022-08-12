using AutoCompleteTextBox.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLauncherApp.Service
{
    public class PathProvideService : IPathSuggestionService
    {
        private readonly List<string> numbers = new();

        public PathProvideService()
        {
            for (int i = 0; i < 10000; i++)
            {
                numbers.Add(i.ToString());
            }
        }

        public IEnumerable GetSuggestions(string filter)
        {
            return numbers.Where(x => x.Contains(filter));
        }
    }

}
