#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class CoreUtility {

	#region Class implementation
	public static Vector2 QuadraticCurve (Vector2 a, Vector2 b, Vector2 tangent, float time) {
		Vector2 point = QuadraticInterpolation (a, b, tangent, time);
		return point;
	}

	public static void DrawQuadraticCurve (Vector2 a, Vector2 b, Vector2 tangent) {
#if UNITY_EDITOR
		for (int i = 0; i < 101; i++) {
			Vector2 prevLine = QuadraticInterpolation (a, b, tangent, ((i > 0) ? i - 1 : i) / 100f);
			Vector2 line = QuadraticInterpolation (a, b, tangent, i / 100f);
			Handles.DrawLine (prevLine, line);
		}
#endif
	}

	private static Vector2 QuadraticInterpolation (Vector2 a, Vector2 b, Vector2 tangent, float time) {
		Vector2 p0 = Vector2.Lerp (a, tangent, time);
		Vector2 p1 = Vector2.Lerp (tangent, b, time);
		return Vector2.Lerp (p0, p1, time);
	}

	public static RaycastHit2D Raycast (Vector2 origin, Vector2 direction, float distance, int layerMask, Color color, bool debug = true) {
		if (debug)
			Debug.DrawRay (origin, direction * distance, color);
		return Physics2D.Raycast (origin, direction, distance, layerMask);
	}

	public static float GetAngle (float x, float y) {
		float value = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
		if (value < 0) value += 360f;
		return value;
	}

	public static bool AngleBetween (float searchedAngle, float angleA, float angleB) {
		angleB = (angleB - angleA) < 0f ? angleB - angleA + 360f : angleB - angleA;
		searchedAngle = (searchedAngle - angleA) < 0f ? searchedAngle - angleA + 360f : searchedAngle - angleA;
		return (searchedAngle < angleB);
	}

	public static Vector3[] GetArcPoints (int count, Vector3 center, float angleA, float angleB, float radius) {
		Vector3[] points = new Vector3[count];
		for (int i = 0; i < count; i++) {
			Quaternion quaternion = Quaternion.AngleAxis (Mathf.LerpAngle (angleA, angleB, (float) i / (count - 1)), Vector3.back);
			points[i] = quaternion * Vector2.right * radius;
			points[i] += center;
		}

		return points;
	}

	public static Vector2 GetRotationPoint (float angle, Vector3 axis, Vector3 direction) {
		return Quaternion.AngleAxis (angle, axis) * direction;
	}

	public static Vector2 GetRotationPoint (float angle, Vector3 axis, Vector3 direction, Vector2 center, float radius) {
		Vector2 vector = Quaternion.AngleAxis (angle, axis) * direction;
		vector.Normalize ();
		vector *= radius;
		vector += center;
		return vector;
	}

	public static void OpenScript (string name) {
#if UNITY_EDITOR
		foreach (var assetPath in AssetDatabase.GetAllAssetPaths ()) {
			if (assetPath.EndsWith (name)) {
				MonoScript script = (MonoScript) AssetDatabase.LoadAssetAtPath (assetPath, typeof (MonoScript));
				if (script != null) {
					AssetDatabase.OpenAsset (script);
					break;
				}
			}
		}
#endif
	}
	#endregion
}