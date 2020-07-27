using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public class AdvancedRectExampleEditorWindow : EditorWindow
	{
		#region Class Accesors
		private Rect Rect
		{
			get { return new Rect (Vector2.zero, position.size); }
		}
		#endregion

		#region Class Overrides
		private void OnGUI ()
		{
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (Rect, AdvancedRect.Orientation.Vertical,
				new AdvancedRect.FixedItem ("Header", 18),
				new AdvancedRect.FixedSpace (2),
				new AdvancedRect.ExpandedGroup (AdvancedRect.Orientation.Horizontal,
					new AdvancedRect.FixedGroup (AdvancedRect.Orientation.Vertical, 150,
						new AdvancedRect.FixedItem ("ListHeader", 18),
						new AdvancedRect.FixedSpace (2),
						new AdvancedRect.ExpandedItem ("List"),
						new AdvancedRect.FixedSpace (2),
						new AdvancedRect.FixedItem ("AddButton", 16)
					),
					new AdvancedRect.FixedSpace (2),
					new AdvancedRect.ExpandedGroup (AdvancedRect.Orientation.Vertical,
						new AdvancedRect.ExpandedItem ("PropertiesA"),
						new AdvancedRect.FixedSpace (2),
						new AdvancedRect.ExpandedItem ("PropertiesB")
					)
				)
			);

			foreach (KeyValuePair<string, Rect> rect in rects)
			{
				EditorGUI.DrawRect (rect.Value, Color.black);
				AdvancedLabel.Draw (rect.Value, new AdvancedLabel.Config (rect.Key));
			}
		}
		#endregion

		#region Class Implementation
		[MenuItem ("Window/GGS Framework/Examples/Advanced Rect")]
		private static void Open ()
		{
			AdvancedRectExampleEditorWindow window = GetWindow<AdvancedRectExampleEditorWindow> ();
			window.titleContent = new GUIContent ("ARect Example");
		}
		#endregion
	}
}