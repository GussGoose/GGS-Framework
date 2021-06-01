#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace GGS_Framework.Editor
{
    public abstract class AdvancedDropdownDataSource
    {
        #region Members
        private static readonly string SearchHeader = L10n.Tr ("Search");

        private AdvancedDropdownItem mainTree;
        private AdvancedDropdownItem searchTree;
        private List<int> selectedIDs = new List<int> ();
        protected List<AdvancedDropdownItem> searchableElements;
        #endregion

        #region Accesors
        public AdvancedDropdownItem MainTree
        {
            get { return mainTree; }
        }

        public AdvancedDropdownItem SearchTree
        {
            get { return searchTree; }
        }

        public List<int> SelectedIDs
        {
            get { return selectedIDs; }
        }

        protected AdvancedDropdownItem Root
        {
            get { return mainTree; }
        }
        #endregion

        #region Implementation
        public void ReloadData ()
        {
            mainTree = FetchData ();
        }

        protected abstract AdvancedDropdownItem FetchData ();

        public void RebuildSearch (string search)
        {
            searchTree = Search (search);
        }

        protected bool AddMatchItem (AdvancedDropdownItem e, string name, string[] searchWords, List<AdvancedDropdownItem> matchesStart, List<AdvancedDropdownItem> matchesWithin)
        {
            bool didMatchAll = true;
            bool didMatchStart = false;

            // See if we match ALL the search words.
            for (int w = 0; w < searchWords.Length; w++)
            {
                string search = searchWords[w];
                if (name.Contains (search))
                {
                    // If the start of the item matches the first search word, make a note of that.
                    if (w == 0 && name.StartsWith (search))
                        didMatchStart = true;
                }
                else
                {
                    // As soon as any word is not matched, we disregard this item.
                    didMatchAll = false;
                    break;
                }
            }

            // We always need to match all search words.
            // If we ALSO matched the start, this item gets priority.
            if (didMatchAll)
            {
                if (didMatchStart)
                    matchesStart.Add (e);
                else
                    matchesWithin.Add (e);
            }

            return didMatchAll;
        }

        protected virtual AdvancedDropdownItem Search (string searchString)
        {
            if (searchableElements == null)
                BuildSearchableElements ();

            if (string.IsNullOrEmpty (searchString))
                return null;

            // Support multiple search words separated by spaces.
            string[] searchWords = searchString.ToLower ().Split (' ');

            // We keep two lists. Matches that matches the start of an item always get first priority.
            List<AdvancedDropdownItem> matchesStart = new List<AdvancedDropdownItem> ();
            List<AdvancedDropdownItem> matchesWithin = new List<AdvancedDropdownItem> ();

            foreach (AdvancedDropdownItem e in searchableElements)
            {
                string name = e.SearchableName.ToLower ().Replace (" ", "");
                AddMatchItem (e, name, searchWords, matchesStart, matchesWithin);
            }

            AdvancedDropdownItem searchTree = new AdvancedDropdownItem (SearchHeader);
            matchesStart.Sort ();
            foreach (AdvancedDropdownItem element in matchesStart)
                searchTree.AddChild (element);

            matchesWithin.Sort ();
            foreach (AdvancedDropdownItem element in matchesWithin)
                searchTree.AddChild (element);

            return searchTree;
        }

        private void BuildSearchableElements ()
        {
            searchableElements = new List<AdvancedDropdownItem> ();
            BuildSearchableElements (Root);
        }

        private void BuildSearchableElements (AdvancedDropdownItem item)
        {
            if (!item.Children.Any ())
            {
                searchableElements.Add (item);
                return;
            }

            foreach (AdvancedDropdownItem child in item.Children)
                BuildSearchableElements (child);
        }
        #endregion
    }
}
#endif // UNITY_EDITOR