using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GGS_Framework.Development.Test
{
	public class ExtendedRectEditorWindow : EditorWindow
	{
		#region Class members
		#endregion

		#region Class accesors
		#endregion

		#region Class overrides
		private void OnGUI ()
		{
			List<Rect> rects = ExtendedRect.GroupRect (new Rect (Vector2.zero, position.size),
				new RectGroup (RectGroupOrientation.Vertical,
					new RectGroup (RectGroupOrientation.Horizontal,
						new RectGroup (RectGroupOrientation.Horizontal,
							new RectLayoutElement ("Box1"),
							new RectLayoutElement (10)
						),
						new RectGroup (RectGroupOrientation.Vertical,
							new RectLayoutElement ("Box2"),
							new RectLayoutElement ("Box3")
						),
						new RectGroup (RectGroupOrientation.Vertical,
							new RectLayoutElement ("Box4"),
							new RectLayoutElement (10)
						),
						new RectGroup (RectGroupOrientation.Vertical,
							new RectLayoutElement ("Box5")
						)
					)
				),
				new RectGroup (RectGroupOrientation.Vertical,
					new RectGroup (RectGroupOrientation.Horizontal,
						new RectGroup (RectGroupOrientation.Horizontal,
							new RectLayoutElement ("Box6"),
							new RectLayoutElement (10)
						),
						new RectGroup (RectGroupOrientation.Vertical,
							new RectGroup (RectGroupOrientation.Vertical),
							new RectGroup (RectGroupOrientation.Horizontal,
								new RectLayoutElement ("Box7"),
								new RectLayoutElement ("Box8")
							)
						),
						new RectGroup (RectGroupOrientation.Vertical,
							new RectLayoutElement ("Box9"),
							new RectLayoutElement (10),
							new RectLayoutElement ("Box10")
						)
					)
				)
			);

			Color[] colors = new Color[] { Color.red, Color.blue, Color.yellow, Color.magenta };
			for (int i = 0; i < rects.Count; i++)
				EditorGUI.DrawRect (rects[i], colors[i]);
		}
		#endregion

		#region Class implementation
		[MenuItem ("Window/GGS Framework/Development/Extended Rect Example")]
		private static void Open ()
		{
			ExtendedRectEditorWindow window = (ExtendedRectEditorWindow) GetWindow (typeof (ExtendedRectEditorWindow));
			window.Show ();
		}
		#endregion
	}
}