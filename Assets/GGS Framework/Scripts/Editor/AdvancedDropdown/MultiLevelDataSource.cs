#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

namespace GGS_Framework.Editor
{
    public class MultiLevelDataSource : AdvancedDropdownDataSource
    {
        #region Members
        private string[] displayedOptions;
        private string label = "";
        private static int selectedIndex;
        #endregion

        #region Accesors
        internal string[] DisplayedOptions
        {
            set { displayedOptions = value; }
        }

        internal string Label
        {
            set { label = value; }
        }

        internal int SelectedIndex
        {
            set { selectedIndex = value; }
        }
        #endregion

        #region Constructors
        internal MultiLevelDataSource ()
        {
        }
        #endregion

        #region Implementation
        public MultiLevelDataSource (string[] displayOptions)
        {
            displayedOptions = displayOptions;
        }
        #endregion

        #region Overrides
        protected override AdvancedDropdownItem FetchData ()
        {
            AdvancedDropdownItem rootGroup = new AdvancedDropdownItem (label);
            searchableElements = new List<AdvancedDropdownItem> ();

            for (int i = 0; i < displayedOptions.Length; i++)
            {
                string menuPath = displayedOptions[i];
                string[] paths = menuPath.Split ('/');

                AdvancedDropdownItem parent = rootGroup;
                for (int j = 0; j < paths.Length; j++)
                {
                    string path = paths[j];
                    if (j == paths.Length - 1)
                    {
                        MultiLevelItem element = new MultiLevelItem (path, menuPath);
                        element.ElementIndex = i;
                        parent.AddChild (element);
                        searchableElements.Add (element);

                        if (i == selectedIndex)
                        {
                            SelectedIDs.Add (element.Id);
//                            var tempParent = parent;
//                            AdvancedDropdownItem searchedItem = element;
                            //TODO fix selecting
//                            while (tempParent != null)
//                            {
//                                state.SetSelectedIndex(tempParent, tempParent.children.IndexOf(searchedItem));
//                                searchedItem = tempParent;
//                                tempParent = tempParent.parent;
//                            }
                        }

                        continue;
                    }

                    string groupPathId = paths[0];
                    for (int k = 1; k <= j; k++)
                        groupPathId += "/" + paths[k];

                    AdvancedDropdownItem group = parent.Children.SingleOrDefault (c => ((MultiLevelItem) c).stringId == groupPathId);
                    if (group == null)
                    {
                        group = new MultiLevelItem (path, groupPathId);
                        parent.AddChild (group);
                    }

                    parent = group;
                }
            }

            return rootGroup;
        }
        #endregion

        #region Nested Classes
        private class MultiLevelItem : AdvancedDropdownItem
        {
            #region Members
            internal string stringId;
            #endregion

            #region Constructors
            public MultiLevelItem (string path, string menuPath) : base (path)
            {
                stringId = menuPath;
                Id = menuPath.GetHashCode ();
            }
            #endregion

            #region Overrides
            public override string ToString ()
            {
                return stringId;
            }
            #endregion
        }
        #endregion
    }
}
#endif // UNITY_EDITOR