using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface ITextSearchService
    {
        public IList<TextSearchResult> Search(string text, string searchedTerm);
    }

    public class TextSearchResult
    {
        public TextSearchResult(int begin, int end)
        {
            Begin = begin;
            End = end;
        }

        public int Begin { get; }
        public int End { get; }
    }
}
