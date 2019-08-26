using UnityEngine;
using System.Collections;

namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        public class ExpandedItem : Element
        {
            #region Class Implementation
            public ExpandedItem (string key, Padding padding) : base (key, SizeType.Expanded, 0, padding, true)
            {
            }

            public ExpandedItem (string key, bool use) : base (key, SizeType.Expanded, 0, null, use)
            {
            }

            public ExpandedItem (string key, Padding padding = null, bool use = true) : base (key, SizeType.Expanded, 0, padding, use)
            {
            }
            #endregion
        }
    }
}