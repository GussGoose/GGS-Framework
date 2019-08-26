using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GGS_Framework
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
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (Rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ProportionalItem ("I1", 10),
				new AdvancedRect.ProportionalItem ("I2", 40),
				new AdvancedRect.ProportionalGroup (AdvancedRect.Orientation.Vertical, 50,
					new AdvancedRect.ProportionalItem ("I3", 20),
					new AdvancedRect.ProportionalItem ("I4", 80)
				)
			);

			foreach (Rect rect in rects.Values)
				EditorGUI.DrawRect (rect.Expand (2), Color.red);
		}
		#endregion

		#region Class Implementation
		[MenuItem ("Window/GGS Framework/Advanced Rect Example")]
		private static void Open ()
		{
			AdvancedRectExampleEditorWindow window = GetWindow<AdvancedRectExampleEditorWindow> ();
			window.titleContent = new GUIContent ("ARect Example");
		}
		#endregion
	}
}