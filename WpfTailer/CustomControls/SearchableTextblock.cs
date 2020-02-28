using Core.Services;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace WpfTailer.CustomControls
{
    public class SearchableTextblock : TextBox
    {
        private readonly ITextSearchService _textSearchService;
        private IList<TextSearchResult> lastSearchResults;

        public SearchableTextblock()
        {
            this._textSearchService = Mvx.IoCProvider.Resolve<ITextSearchService>();
        }


        #region OccurencesProperty
        private static readonly DependencyPropertyKey OccurencesPropertyKey =
                                                  DependencyProperty.RegisterReadOnly(nameof(Occurences), typeof(int), typeof(SearchableTextblock),
                                                      new PropertyMetadata(0));
        public static readonly DependencyProperty OccurencesProperty = OccurencesPropertyKey.DependencyProperty;

        public int Occurences
        {
            get
            {
                return (int)GetValue(OccurencesProperty);
            }
            protected set
            {
                SetValue(OccurencesPropertyKey, value);
            }
        }
        #endregion

        #region SelectedOccurence
        private static readonly DependencyPropertyKey SelectedOccurencePropertyKey =
                                                   DependencyProperty.RegisterReadOnly(
                                                       nameof(SelectedOccurence),
                                                       typeof(int),
                                                       typeof(SearchableTextblock),
                                                       new PropertyMetadata(0));
        public static readonly DependencyProperty SelectedOccurenceProperty = SelectedOccurencePropertyKey.DependencyProperty;

        public int SelectedOccurence
        {
            get
            {
                return (int)GetValue(SelectedOccurenceProperty);
            }
            protected set
            {
                SetValue(SelectedOccurencePropertyKey, value);
            }
        }
        #endregion

        #region SearchedTextProperty
        public static readonly DependencyProperty SearchedTextProperty =
                                DependencyProperty.Register(nameof(SearchedText),
                                typeof(string),
                                typeof(SearchableTextblock),
                                new PropertyMetadata(null, SearchedTextHandler));

        public string SearchedText
        {
            get
            {
                return (string)GetValue(SearchedTextProperty);
            }
            set
            {
                SetValue(SearchedTextProperty, value);
            }
        }
        #endregion

        private static void SearchedTextHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is SearchableTextblock searchableTextblock)
            {
                searchableTextblock.HandleSearchTextChange();
            }
        }

        public void HandleSearchTextChange()
        {
            UpdateSearchResults();
            SelectCurrentSearchResults();
        }

        private void UpdateSearchResults()
        {
            try
            {
                lastSearchResults = this._textSearchService.Search(this.Text, this.SearchedText);
                Occurences = lastSearchResults.Count;
                if(Occurences == 0)
                {
                    SelectedOccurence = 0;
                    return;
                }

                SelectedOccurence = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SelectCurrentSearchResults()
        {
            if(lastSearchResults == null || lastSearchResults.Count < this.SelectedOccurence - 1)
            {
                //TODO: Consider how to handle text updates
                UpdateSearchResults();
                return;
            }

            var resultsToSelect = this.lastSearchResults[SelectedOccurence - 1];
            this.Select(resultsToSelect.Begin, resultsToSelect.End);
        }
    }
}
