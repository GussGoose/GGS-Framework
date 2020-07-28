#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;

namespace GGS_Framework.Editor
{
    [Serializable]
    public class AdvancedDropdownState
    {
        #region Members
        [SerializeField] private AdvancedDropdownItemState[] states = new AdvancedDropdownItemState[0];
        private AdvancedDropdownItemState lastSelectedState;
        #endregion

        #region Implementation
        private AdvancedDropdownItemState GetStateForItem (AdvancedDropdownItem item)
        {
            if (lastSelectedState != null && lastSelectedState.itemId == item.Id)
                return lastSelectedState;
            
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i].itemId == item.Id)
                {
                    lastSelectedState = states[i];
                    return lastSelectedState;
                }
            }

            Array.Resize (ref states, states.Length + 1);
            states[states.Length - 1] = new AdvancedDropdownItemState (item);
            lastSelectedState = states[states.Length - 1];
            return states[states.Length - 1];
        }

        internal void MoveDownSelection (AdvancedDropdownItem item)
        {
            AdvancedDropdownItemState state = GetStateForItem (item);
            int selectedIndex = state.selectedIndex;
            do
                ++selectedIndex;
            while
                (selectedIndex < item.Children.Count () && item.Children.ElementAt (selectedIndex).IsSeparator ());

            if (selectedIndex >= item.Children.Count ())
                selectedIndex = 0;

            if (selectedIndex < item.Children.Count ())
                SetSelectionOnItem (item, selectedIndex);
        }

        internal void MoveUpSelection (AdvancedDropdownItem item)
        {
            AdvancedDropdownItemState state = GetStateForItem (item);
            int selectedIndex = state.selectedIndex;
            do
                --selectedIndex;
            while
                (selectedIndex >= 0 && item.Children.ElementAt (selectedIndex).IsSeparator ());

            if (selectedIndex < 0)
                selectedIndex = item.Children.Count () - 1;

            if (selectedIndex >= 0)
                SetSelectionOnItem (item, selectedIndex);
        }

        internal void SetSelectionOnItem (AdvancedDropdownItem item, int selectedIndex)
        {
            AdvancedDropdownItemState state = GetStateForItem (item);

            if (selectedIndex < 0)
                state.selectedIndex = 0;
            else if (selectedIndex >= item.Children.Count ())
                state.selectedIndex = item.Children.Count () - 1;
            else
                state.selectedIndex = selectedIndex;
        }

        internal int GetSelectedIndex (AdvancedDropdownItem item)
        {
            return GetStateForItem (item).selectedIndex;
        }

        internal void SetSelectedIndex (AdvancedDropdownItem item, int index)
        {
            GetStateForItem (item).selectedIndex = index;
        }

        internal AdvancedDropdownItem GetSelectedChild (AdvancedDropdownItem item)
        {
            int index = GetSelectedIndex (item);
            if (!item.Children.Any () || index < 0 || index >= item.Children.Count ())
                return null;
            return item.Children.ElementAt (index);
        }

        internal Vector2 GetScrollState (AdvancedDropdownItem item)
        {
            return GetStateForItem (item).scroll;
        }

        internal void SetScrollState (AdvancedDropdownItem item, Vector2 scrollState)
        {
            GetStateForItem (item).scroll = scrollState;
        }
        #endregion

        #region Nested Classes
        [Serializable]
        private class AdvancedDropdownItemState
        {
            #region Members
            public int itemId;
            public int selectedIndex = -1;
            public Vector2 scroll;
            #endregion

            #region Constructors
            public AdvancedDropdownItemState (AdvancedDropdownItem item)
            {
                itemId = item.Id;
            }
            #endregion
        }
        #endregion
    }
}
#endif // UNITY_EDITOR