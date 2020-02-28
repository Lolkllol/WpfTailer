using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class TextSearchService : ITextSearchService
    {
        public IList<TextSearchResult> Search(string text, string searchedTerm)
        {
            var results = new List<TextSearchResult>();

            results.Add(new TextSearchResult(1, 3));
            //TODO: Add logic

            return results;
        }
    }
}
