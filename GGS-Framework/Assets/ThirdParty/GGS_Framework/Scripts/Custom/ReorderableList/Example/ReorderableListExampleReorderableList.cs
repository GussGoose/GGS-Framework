#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
    public class ReorderableListExampleReorderableList : ReorderableList<ReorderableListExampleStruct>
    {
        #region Class Members
        #endregion

        #region Class Accesors
        #endregion

        #region Class Overrides
        protected override void DrawElement (Rect rect, int index)
        {
            GUI.Label (rect, list[index].displayName);
        }

        protected override string GetNameOfElementForSearch (int elementIndex)
        {
            return list[elementIndex].displayName;
        }

        protected override ReorderableListExampleStruct CreateElementObject ()
        {
            return new ReorderableListExampleStruct ();
        }
        #endregion

        #region Class Implementation
        public ReorderableListExampleReorderableList (List<ReorderableListExampleStruct> list) : base (list)
        {
        }
        #endregion
    }
}
#endif