#if UNITY_EDITOR
using UnityEditor; 
#endif
using UnityEngine;

public class ExtendedGizmos {

	#region Class implementation
	public static void DrawTextPoint (Vector2 position, string text) {
#if UNITY_EDITOR
		SceneView sceneView = SceneView.currentDrawingSceneView;
		Rect sceneViewRect = sceneView.camera.pixelRect;

		Handles.DrawWireDisc (position, Vector3.back, 0.5f);
		Handles.DrawLine (position + Vector2.up * 0.5f, position + Vector2.down * 0.5f);
		Handles.DrawLine (position + Vector2.left * 0.5f, position + Vector2.right * 0.5f);

		Handles.BeginGUI ();

		Vector2 boxPosition = sceneView.camera.WorldToScreenPoint (position + Vector2.down * 0.75f);
		Vector2 boxSize = GUI.skin.label.CalcSize (new GUIContent (text)) + Vector2.right;
		//boxSize /= sceneView.size;
		//boxSize *= 50;
		Rect boxRect = new Rect (new Vector2 (boxPosition.x - boxSize.x / 2f, -boxPosition.y + sceneViewRect.height), boxSize);

		GUI.Box (boxRect, "", EditorStyles.helpBox);

		GUIStyle style = new GUIStyle (GUI.skin.label);
		style.fontStyle = FontStyle.Normal;
		style.alignment = TextAnchor.MiddleCenter;
		style.clipping = TextClipping.Overflow;
		//style.fontSize = 500 / (int) sceneView.size;
		GUI.Label (boxRect, new GUIContent (text), style);

		Handles.EndGUI (); 
#endif
	}

	public static void DrawTextPoint (Vector2 position, string text, Color color) {
#if UNITY_EDITOR
		SceneView sceneView = SceneView.currentDrawingSceneView;
		Rect sceneViewRect = sceneView.camera.pixelRect;

		Handles.color = color;
		Handles.DrawWireDisc (position, Vector3.back, 0.5f);
		Handles.DrawLine (position + Vector2.up * 0.5f, position + Vector2.down * 0.5f);
		Handles.DrawLine (position + Vector2.left * 0.5f, position + Vector2.right * 0.5f);
		Handles.color = Color.white;

		Handles.BeginGUI ();

		Vector2 boxPosition = sceneView.camera.WorldToScreenPoint (position + Vector2.down * 0.75f);
		Vector2 boxSize = GUI.skin.label.CalcSize (new GUIContent (text)) + Vector2.right;
		//boxSize /= sceneView.size;
		//boxSize *= 50;
		Rect boxRect = new Rect (new Vector2 (boxPosition.x - boxSize.x / 2f, -boxPosition.y + sceneViewRect.height), boxSize);

		GUI.Box (boxRect, "", EditorStyles.helpBox);

		GUIStyle style = new GUIStyle (GUI.skin.label);
		style.fontStyle = FontStyle.Normal;
		style.alignment = TextAnchor.MiddleCenter;
		style.clipping = TextClipping.Overflow;
		//style.fontSize = 500 / (int) sceneView.size;
		GUI.Label (boxRect, new GUIContent (text), style);

		Handles.EndGUI (); 
#endif
	}

	public static void DrawBracketIndicator (Vector2 startPosition, Vector2 endPosition, string text, Color color, float spacing = 1, float arrowSize = 2) {
#if UNITY_EDITOR
		Vector2 lineDirection = endPosition - startPosition;
		Vector2 perpLineDirection = new Vector2 (lineDirection.y, -lineDirection.x).normalized;
		Vector2 startSpacedPos = startPosition + perpLineDirection * spacing;
		Vector2 endSpacedPos = endPosition + perpLineDirection * spacing;
		Vector2 middleSpacedPos = startSpacedPos + lineDirection / 2f;

		Vector2 arrowOffset = lineDirection.normalized * arrowSize / 2f;
		Vector2 startArrowPos = middleSpacedPos - arrowOffset;
		Vector2 endArrowPos = middleSpacedPos + arrowOffset;
		Vector2 middleArrowPos = middleSpacedPos + perpLineDirection * arrowSize / 2f;

		Handles.color = color;
		Handles.DrawLine (startPosition, startSpacedPos);
		Handles.DrawLine (endPosition, endSpacedPos);

		Handles.DrawLine (startSpacedPos, startArrowPos);
		Handles.DrawLine (endSpacedPos, endArrowPos);
		Handles.DrawLine (startArrowPos, middleArrowPos);
		Handles.DrawLine (endArrowPos, middleArrowPos);
		Handles.color = Color.white;

		SceneView sceneView = SceneView.currentDrawingSceneView;
		Rect sceneViewRect = sceneView.camera.pixelRect;

		Handles.BeginGUI ();

		Vector2 boxPosition = sceneView.camera.WorldToScreenPoint (middleArrowPos + perpLineDirection * 0.2f);
		Vector2 boxSize = GUI.skin.label.CalcSize (new GUIContent (text)) + Vector2.right;
		boxPosition.x += boxSize.x / 2f * perpLineDirection.x;
		boxPosition.y += boxSize.y / 2f * perpLineDirection.y;

		//boxSize /= sceneView.size;
		//boxSize *= 50;
		Rect boxRect = new Rect (new Vector2 (boxPosition.x - boxSize.x / 2f, (-boxPosition.y - boxSize.y / 2f) + sceneViewRect.height), boxSize);

		GUI.Box (boxRect, "", EditorStyles.helpBox);

		GUIStyle style = new GUIStyle (GUI.skin.label);
		style.alignment = TextAnchor.MiddleCenter;
		//style.fontSize = 500 / (int) sceneView.size;
		GUI.Label (boxRect, new GUIContent (text), style);

		Handles.EndGUI ();
#endif
	}

	public static void DrawSolidCircle (Vector3 center, Vector3 normal, float radius, Color color) {
#if UNITY_EDITOR
		Handles.color = color;
		Handles.DrawSolidDisc (center, normal, radius);
#endif
	}
	#endregion
}