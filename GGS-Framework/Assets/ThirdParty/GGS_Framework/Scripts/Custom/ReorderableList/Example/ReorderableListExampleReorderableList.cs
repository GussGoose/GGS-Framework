#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public class ReorderableListExampleReorderableList : ReorderableList<ReorderableListExampleStruct>
	{
		#region Class Overrides
		protected override void DrawElement (Rect rect, int index)
		{
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ProportionalItem ("Name", 50),
				new AdvancedRect.FixedSpace (2),
				new AdvancedRect.ExpandedItem ("Float"),
				new AdvancedRect.FixedSpace (2),
				new AdvancedRect.FixedItem ("Bool", 50)
			);
			
			EditorGUIUtility.labelWidth = 35;

			ReorderableListExampleStruct element = list[index];
			element.exampleName = EditorGUI.TextField (rects["Name"], element.exampleName);
			element.exampleFloat = EditorGUI.Slider (rects["Float"], "Float", element.exampleFloat, -10, 10);
			element.exampleBool = EditorGUI.Toggle (rects["Bool"], "Bool", element.exampleBool);

			EditorGUIUtility.labelWidth = 0;
		}

		protected override string GetNameOfElementForSearch (int index)
		{
			return list[index].exampleName;
		}

		protected override ReorderableListExampleStruct CreateElementObject ()
		{
			return new ReorderableListExampleStruct ();
		}
		#endregion

		#region Class Implementation
		public ReorderableListExampleReorderableList (ReorderableListState state, List<ReorderableListExampleStruct> list) : base (state, list)
		{
			ShowAlternatingRowBackgrounds = true;
		}
		#endregion
	}
}
#endif