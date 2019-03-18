#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class ExtendedGizmos {

	#region Class implementation
	public static void DrawTextPoint (Vector3 position, string text) {
#if UNITY_EDITOR
		SceneView sceneView = SceneView.currentDrawingSceneView;
		Camera camera = sceneView.camera;
		Rect sceneViewRect = camera.pixelRect;

		Handles.BeginGUI ();

		Vector2 size = CoreUtilityStyles.TextPoint.CalcSize (new GUIContent (text));
		Rect sizedRect = HandleUtility.WorldPointToSizedRect (position, GUIContent.none, GUIStyle.none);
		sizedRect.size = size;
		sizedRect.position = sizedRect.position - size / 2f;
		GUI.Label (sizedRect, new GUIContent (text), CoreUtilityStyles.TextPoint);

		Handles.EndGUI ();
#endif
	}

	public static void DrawBracketIndicator (Vector3 startPosition, Vector3 endPosition, string text, Color color, float spacing = 1, float arrowSize = 2) {
#if UNITY_EDITOR
		Vector3 lineDirection = endPosition - startPosition;
		Vector3 perpLineDirection = new Vector3 (lineDirection.y, -lineDirection.x).normalized;
		Vector3 startSpacedPos = startPosition + perpLineDirection * spacing;
		Vector3 endSpacedPos = endPosition + perpLineDirection * spacing;
		Vector3 middleSpacedPos = startSpacedPos + lineDirection / 2f;

		Vector3 arrowOffset = lineDirection.normalized * arrowSize / 2f;
		Vector3 startArrowPos = middleSpacedPos - arrowOffset;
		Vector3 endArrowPos = middleSpacedPos + arrowOffset;
		Vector3 middleArrowPos = middleSpacedPos + perpLineDirection * arrowSize / 2f;

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

		DrawTextPoint (middleArrowPos, text);
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