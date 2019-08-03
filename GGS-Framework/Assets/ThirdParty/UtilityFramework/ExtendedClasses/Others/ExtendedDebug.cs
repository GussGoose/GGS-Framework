namespace UtilityFramework
{
	using UnityEngine;

	public static class ExtendedDebug
	{
		#region Class implementation
		#region Debug Draw Functions
		#region Point
		public static void DebugPoint (Vector3 position, Color color, float scale = 1, float duration = 0, bool depthTest = true)
		{
			color = (color == default (Color)) ? Color.white : color;

			Debug.DrawRay (position + (Vector3.up * (scale * 0.5f)), -Vector3.up * scale, color, duration, depthTest);
			Debug.DrawRay (position + (Vector3.right * (scale * 0.5f)), -Vector3.right * scale, color, duration, depthTest);
			Debug.DrawRay (position + (Vector3.forward * (scale * 0.5f)), -Vector3.forward * scale, color, duration, depthTest);
		}

		public static void DebugPoint (Vector3 position, float scale = 1, float duration = 0, bool depthTest = true)
		{
			DebugPoint (position, Color.white, scale, duration, depthTest);
		}
		#endregion

		#region Bounds
		public static void DoDebugBounds (Bounds bounds, Color color, float duration = 0, bool depthTest = true)
		{
			Vector3 center = bounds.center;

			float x = bounds.extents.x;
			float y = bounds.extents.y;
			float z = bounds.extents.z;

			Vector3 ruf = center + new Vector3 (x, y, z);
			Vector3 rub = center + new Vector3 (x, y, -z);
			Vector3 luf = center + new Vector3 (-x, y, z);
			Vector3 lub = center + new Vector3 (-x, y, -z);

			Vector3 rdf = center + new Vector3 (x, -y, z);
			Vector3 rdb = center + new Vector3 (x, -y, -z);
			Vector3 lfd = center + new Vector3 (-x, -y, z);
			Vector3 lbd = center + new Vector3 (-x, -y, -z);

			Debug.DrawLine (ruf, luf, color, duration, depthTest);
			Debug.DrawLine (ruf, rub, color, duration, depthTest);
			Debug.DrawLine (luf, lub, color, duration, depthTest);
			Debug.DrawLine (rub, lub, color, duration, depthTest);

			Debug.DrawLine (ruf, rdf, color, duration, depthTest);
			Debug.DrawLine (rub, rdb, color, duration, depthTest);
			Debug.DrawLine (luf, lfd, color, duration, depthTest);
			Debug.DrawLine (lub, lbd, color, duration, depthTest);

			Debug.DrawLine (rdf, lfd, color, duration, depthTest);
			Debug.DrawLine (rdf, rdb, color, duration, depthTest);
			Debug.DrawLine (lfd, lbd, color, duration, depthTest);
			Debug.DrawLine (lbd, rdb, color, duration, depthTest);
		}

		public static void DebugBounds (Bounds bounds, float duration = 0, bool depthTest = true)
		{
			DoDebugBounds (bounds, Color.white, duration, depthTest);
		}

		public static void DebugBounds (Bounds bounds, Color color, float duration = 0, bool depthTest = true)
		{
			DoDebugBounds (bounds, color, duration, depthTest);
		}
		#endregion

		#region Local
		public static void DebugLocalCube (Transform transform, Vector3 size, Color color, Vector3 center = default (Vector3), float duration = 0, bool depthTest = true)
		{
			Vector3 lbb = transform.TransformPoint (center + ((-size) * 0.5f));
			Vector3 rbb = transform.TransformPoint (center + (new Vector3 (size.x, -size.y, -size.z) * 0.5f));

			Vector3 lbf = transform.TransformPoint (center + (new Vector3 (size.x, -size.y, size.z) * 0.5f));
			Vector3 rbf = transform.TransformPoint (center + (new Vector3 (-size.x, -size.y, size.z) * 0.5f));

			Vector3 lub = transform.TransformPoint (center + (new Vector3 (-size.x, size.y, -size.z) * 0.5f));
			Vector3 rub = transform.TransformPoint (center + (new Vector3 (size.x, size.y, -size.z) * 0.5f));

			Vector3 luf = transform.TransformPoint (center + ((size) * 0.5f));
			Vector3 ruf = transform.TransformPoint (center + (new Vector3 (-size.x, size.y, size.z) * 0.5f));

			Debug.DrawLine (lbb, rbb, color, duration, depthTest);
			Debug.DrawLine (rbb, lbf, color, duration, depthTest);
			Debug.DrawLine (lbf, rbf, color, duration, depthTest);
			Debug.DrawLine (rbf, lbb, color, duration, depthTest);

			Debug.DrawLine (lub, rub, color, duration, depthTest);
			Debug.DrawLine (rub, luf, color, duration, depthTest);
			Debug.DrawLine (luf, ruf, color, duration, depthTest);
			Debug.DrawLine (ruf, lub, color, duration, depthTest);

			Debug.DrawLine (lbb, lub, color, duration, depthTest);
			Debug.DrawLine (rbb, rub, color, duration, depthTest);
			Debug.DrawLine (lbf, luf, color, duration, depthTest);
			Debug.DrawLine (rbf, ruf, color, duration, depthTest);
		}

		public static void DebugLocalCube (Transform transform, Vector3 size, Vector3 center = default (Vector3), float duration = 0, bool depthTest = true)
		{
			DebugLocalCube (transform, size, Color.white, center, duration, depthTest);
		}

		public static void DebugLocalCube (Matrix4x4 space, Vector3 size, Color color, Vector3 center = default (Vector3), float duration = 0, bool depthTest = true)
		{
			color = (color == default (Color)) ? Color.white : color;

			Vector3 lbb = space.MultiplyPoint3x4 (center + ((-size) * 0.5f));
			Vector3 rbb = space.MultiplyPoint3x4 (center + (new Vector3 (size.x, -size.y, -size.z) * 0.5f));

			Vector3 lbf = space.MultiplyPoint3x4 (center + (new Vector3 (size.x, -size.y, size.z) * 0.5f));
			Vector3 rbf = space.MultiplyPoint3x4 (center + (new Vector3 (-size.x, -size.y, size.z) * 0.5f));

			Vector3 lub = space.MultiplyPoint3x4 (center + (new Vector3 (-size.x, size.y, -size.z) * 0.5f));
			Vector3 rub = space.MultiplyPoint3x4 (center + (new Vector3 (size.x, size.y, -size.z) * 0.5f));

			Vector3 luf = space.MultiplyPoint3x4 (center + ((size) * 0.5f));
			Vector3 ruf = space.MultiplyPoint3x4 (center + (new Vector3 (-size.x, size.y, size.z) * 0.5f));

			Debug.DrawLine (lbb, rbb, color, duration, depthTest);
			Debug.DrawLine (rbb, lbf, color, duration, depthTest);
			Debug.DrawLine (lbf, rbf, color, duration, depthTest);
			Debug.DrawLine (rbf, lbb, color, duration, depthTest);

			Debug.DrawLine (lub, rub, color, duration, depthTest);
			Debug.DrawLine (rub, luf, color, duration, depthTest);
			Debug.DrawLine (luf, ruf, color, duration, depthTest);
			Debug.DrawLine (ruf, lub, color, duration, depthTest);

			Debug.DrawLine (lbb, lub, color, duration, depthTest);
			Debug.DrawLine (rbb, rub, color, duration, depthTest);
			Debug.DrawLine (lbf, luf, color, duration, depthTest);
			Debug.DrawLine (rbf, ruf, color, duration, depthTest);
		}

		public static void DebugLocalCube (Matrix4x4 space, Vector3 size, Vector3 center = default (Vector3), float duration = 0, bool depthTest = true)
		{
			DebugLocalCube (space, size, Color.white, center, duration, depthTest);
		}
		#endregion

		#region Circle
		public static void DebugCircle (Vector3 position, Vector3 up, Color color, float radius = 1, float duration = 0, bool depthTest = true)
		{
			up = up.normalized * radius;
			Vector3 forward = Vector3.Slerp (up, -up, 0.5f);
			Vector3 right = Vector3.Cross (up, forward).normalized * radius;

			Matrix4x4 matrix = new Matrix4x4 ();

			matrix[0] = right.x;
			matrix[1] = right.y;
			matrix[2] = right.z;

			matrix[4] = up.x;
			matrix[5] = up.y;
			matrix[6] = up.z;

			matrix[8] = forward.x;
			matrix[9] = forward.y;
			matrix[10] = forward.z;

			Vector3 lastPoint = position + matrix.MultiplyPoint3x4 (new Vector3 (Mathf.Cos (0), 0, Mathf.Sin (0)));
			Vector3 nextPoint = Vector3.zero;

			color = (color == default (Color)) ? Color.white : color;

			for (var i = 0; i < 91; i++)
			{
				nextPoint.x = Mathf.Cos ((i * 4) * Mathf.Deg2Rad);
				nextPoint.z = Mathf.Sin ((i * 4) * Mathf.Deg2Rad);
				nextPoint.y = 0;

				nextPoint = position + matrix.MultiplyPoint3x4 (nextPoint);

				Debug.DrawLine (lastPoint, nextPoint, color, duration, depthTest);
				lastPoint = nextPoint;
			}
		}

		public static void DebugCircle (Vector3 position, Color color, float radius = 1, float duration = 0, bool depthTest = true)
		{
			DebugCircle (position, Vector3.up, color, radius, duration, depthTest);
		}

		public static void DebugCircle (Vector3 position, Vector3 up, float radius = 1, float duration = 0, bool depthTest = true)
		{
			DebugCircle (position, up, Color.white, radius, duration, depthTest);
		}

		public static void DebugCircle (Vector3 position, float radius = 1, float duration = 0, bool depthTest = true)
		{
			DebugCircle (position, Vector3.up, Color.white, radius, duration, depthTest);
		}
		#endregion

		#region Wire Sphere
		public static void DebugWireSphere (Vector3 position, Color color, float radius = 1, float duration = 0, bool depthTest = true)
		{
			float angle = 10;

			Vector3 x = new Vector3 (position.x, position.y + radius * Mathf.Sin (0), position.z + radius * Mathf.Cos (0));
			Vector3 y = new Vector3 (position.x + radius * Mathf.Cos (0), position.y, position.z + radius * Mathf.Sin (0));
			Vector3 z = new Vector3 (position.x + radius * Mathf.Cos (0), position.y + radius * Mathf.Sin (0), position.z);

			Vector3 newX;
			Vector3 newY;
			Vector3 newZ;

			for (int i = 1; i < 37; i++)
			{
				newX = new Vector3 (position.x, position.y + radius * Mathf.Sin (angle * i * Mathf.Deg2Rad), position.z + radius * Mathf.Cos (angle * i * Mathf.Deg2Rad));
				newY = new Vector3 (position.x + radius * Mathf.Cos (angle * i * Mathf.Deg2Rad), position.y, position.z + radius * Mathf.Sin (angle * i * Mathf.Deg2Rad));
				newZ = new Vector3 (position.x + radius * Mathf.Cos (angle * i * Mathf.Deg2Rad), position.y + radius * Mathf.Sin (angle * i * Mathf.Deg2Rad), position.z);

				Debug.DrawLine (x, newX, color, duration, depthTest);
				Debug.DrawLine (y, newY, color, duration, depthTest);
				Debug.DrawLine (z, newZ, color, duration, depthTest);

				x = newX;
				y = newY;
				z = newZ;
			}
		}

		public static void DebugWireSphere (Vector3 position, float radius = 1, float duration = 0, bool depthTest = true)
		{
			DebugWireSphere (position, Color.white, radius, duration, depthTest);
		}
		#endregion

		#region Cylinder
		public static void DebugCylinder (Vector3 start, Vector3 end, Color color, float radius = 1, float duration = 0, bool depthTest = true)
		{
			Vector3 up = (end - start).normalized * radius;
			Vector3 forward = Vector3.Slerp (up, -up, 0.5f);
			Vector3 right = Vector3.Cross (up, forward).normalized * radius;

			//Radial circles
			ExtendedDebug.DebugCircle (start, up, color, radius, duration, depthTest);
			ExtendedDebug.DebugCircle (end, -up, color, radius, duration, depthTest);
			ExtendedDebug.DebugCircle ((start + end) * 0.5f, up, color, radius, duration, depthTest);

			//Side lines
			Debug.DrawLine (start + right, end + right, color, duration, depthTest);
			Debug.DrawLine (start - right, end - right, color, duration, depthTest);

			Debug.DrawLine (start + forward, end + forward, color, duration, depthTest);
			Debug.DrawLine (start - forward, end - forward, color, duration, depthTest);

			//Start endcap
			Debug.DrawLine (start - right, start + right, color, duration, depthTest);
			Debug.DrawLine (start - forward, start + forward, color, duration, depthTest);

			//End endcap
			Debug.DrawLine (end - right, end + right, color, duration, depthTest);
			Debug.DrawLine (end - forward, end + forward, color, duration, depthTest);
		}

		public static void DebugCylinder (Vector3 start, Vector3 end, float radius = 1, float duration = 0, bool depthTest = true)
		{
			DebugCylinder (start, end, Color.white, radius, duration, depthTest);
		}
		#endregion

		#region Cone
		public static void DebugCone (Vector3 position, Vector3 direction, Color color, float angle = 45, float duration = 0, bool depthTest = true)
		{
			float length = direction.magnitude;

			Vector3 forward = direction;
			Vector3 up = Vector3.Slerp (forward, -forward, 0.5f);
			Vector3 right = Vector3.Cross (forward, up).normalized * length;

			direction = direction.normalized;

			Vector3 slerpedVector = Vector3.Slerp (forward, up, angle / 90);

			float dist;
			var farPlane = new Plane (-direction, position + forward);
			var distRay = new Ray (position, slerpedVector);

			farPlane.Raycast (distRay, out dist);

			Debug.DrawRay (position, slerpedVector.normalized * dist, color);
			Debug.DrawRay (position, Vector3.Slerp (forward, -up, angle / 90).normalized * dist, color, duration, depthTest);
			Debug.DrawRay (position, Vector3.Slerp (forward, right, angle / 90).normalized * dist, color, duration, depthTest);
			Debug.DrawRay (position, Vector3.Slerp (forward, -right, angle / 90).normalized * dist, color, duration, depthTest);

			DebugCircle (position + forward, direction, color, (forward - (slerpedVector.normalized * dist)).magnitude, duration, depthTest);
			DebugCircle (position + (forward * 0.5f), direction, color, ((forward * 0.5f) - (slerpedVector.normalized * (dist * 0.5f))).magnitude, duration, depthTest);
		}

		public static void DebugCone (Vector3 position, Vector3 direction, float angle = 45, float duration = 0, bool depthTest = true)
		{
			DebugCone (position, direction, Color.white, angle, duration, depthTest);
		}

		public static void DebugCone (Vector3 position, Color color, float angle = 45, float duration = 0, bool depthTest = true)
		{
			DebugCone (position, Vector3.up, color, angle, duration, depthTest);
		}

		public static void DebugCone (Vector3 position, float angle = 45, float duration = 0, bool depthTest = true)
		{
			DebugCone (position, Vector3.up, Color.white, angle, duration, depthTest);
		}
		#endregion

		#region Arrow
		public static void DebugArrow (Vector3 position, Vector3 direction, Color color, float duration = 0, bool depthTest = true)
		{
			Debug.DrawRay (position, direction, color, duration, depthTest);
			DebugCone (position + direction, -direction * 0.25f, color, 15, duration, depthTest);
		}

		public static void DebugArrow (Vector3 position, Vector3 direction, float duration = 0, bool depthTest = true)
		{
			DebugArrow (position, direction, Color.white, duration, depthTest);
		}
		#endregion

		#region Capsule
		public static void DebugCapsule (Vector3 start, Vector3 end, Color color, float radius = 1, float duration = 0, bool depthTest = true)
		{
			Vector3 up = (end - start).normalized * radius;
			Vector3 forward = Vector3.Slerp (up, -up, 0.5f);
			Vector3 right = Vector3.Cross (up, forward).normalized * radius;

			float height = (start - end).magnitude;
			float sideLength = Mathf.Max (0, (height * 0.5f) - radius);
			Vector3 middle = (end + start) * 0.5f;

			start = middle + ((start - middle).normalized * sideLength);
			end = middle + ((end - middle).normalized * sideLength);

			//Radial circles
			ExtendedDebug.DebugCircle (start, up, color, radius, duration, depthTest);
			ExtendedDebug.DebugCircle (end, -up, color, radius, duration, depthTest);

			//Side lines
			Debug.DrawLine (start + right, end + right, color, duration, depthTest);
			Debug.DrawLine (start - right, end - right, color, duration, depthTest);

			Debug.DrawLine (start + forward, end + forward, color, duration, depthTest);
			Debug.DrawLine (start - forward, end - forward, color, duration, depthTest);

			for (int i = 1; i < 26; i++)
			{
				//Start endcap
				Debug.DrawLine (Vector3.Slerp (right, -up, i / 25) + start, Vector3.Slerp (right, -up, (i - 1) / 25) + start, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (-right, -up, i / 25) + start, Vector3.Slerp (-right, -up, (i - 1) / 25) + start, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (forward, -up, i / 25) + start, Vector3.Slerp (forward, -up, (i - 1) / 25) + start, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (-forward, -up, i / 25) + start, Vector3.Slerp (-forward, -up, (i - 1) / 25) + start, color, duration, depthTest);

				//End endcap
				Debug.DrawLine (Vector3.Slerp (right, up, i / 25) + end, Vector3.Slerp (right, up, (i - 1) / 25) + end, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (-right, up, i / 25) + end, Vector3.Slerp (-right, up, (i - 1) / 25) + end, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (forward, up, i / 25) + end, Vector3.Slerp (forward, up, (i - 1) / 25) + end, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (-forward, up, i / 25) + end, Vector3.Slerp (-forward, up, (i - 1) / 25) + end, color, duration, depthTest);
			}
		}

		public static void DebugCapsule (Vector3 start, Vector3 end, float radius = 1, float duration = 0, bool depthTest = true)
		{
			DebugCapsule (start, end, Color.white, radius, duration, depthTest);
		}

		public static void DebugCapsule2D (Vector3 start, Vector3 end, Color color, float radius = 1, float duration = 0, bool depthTest = true)
		{
			Vector3 up = (end - start).normalized * radius;
			Vector3 forward = Vector3.Slerp (up, -up, 0.5f);
			Vector3 right = Vector3.Cross (up, forward).normalized * radius;

			float height = (start - end).magnitude;
			float sideLength = Mathf.Max (0, (height * 0.5f) - radius);
			Vector3 middle = (end + start) * 0.5f;

			start = middle + ((start - middle).normalized * sideLength);
			end = middle + ((end - middle).normalized * sideLength);

			//Side lines
			Debug.DrawLine (start + right, end + right, color, duration, depthTest);
			Debug.DrawLine (start - right, end - right, color, duration, depthTest);

			for (int i = 1; i < 26; i++)
			{
				//Start endcap
				Debug.DrawLine (Vector3.Slerp (right, -up, i / 25) + start, Vector3.Slerp (right, -up, (i - 1) / 25) + start, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (-right, -up, i / 25) + start, Vector3.Slerp (-right, -up, (i - 1) / 25) + start, color, duration, depthTest);

				//End endcap
				Debug.DrawLine (Vector3.Slerp (right, up, i / 25) + end, Vector3.Slerp (right, up, (i - 1) / 25) + end, color, duration, depthTest);
				Debug.DrawLine (Vector3.Slerp (-right, up, i / 25) + end, Vector3.Slerp (-right, up, (i - 1) / 25) + end, color, duration, depthTest);
			}
		}
		#endregion
		#endregion
		#endregion
	}
}