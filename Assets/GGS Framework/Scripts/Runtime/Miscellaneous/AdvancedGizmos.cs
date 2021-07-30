using UnityEngine;

namespace GGS_Framework
{
    public static class AdvancedGizmos
    {
        #region Class Implementation
        public static void TextPoint (Vector3 position, string text)
        {
            #if UNITY_EDITOR
            UnityEditor.Handles.BeginGUI ();

            GUIStyle textPointStyle = Editor.GGS_FrameworkEditorStyles.GeneralSkin.GetStyle ("TextPoint");
            Vector2 size = textPointStyle.CalcSize (new GUIContent (text));
            Rect sizedRect = UnityEditor.HandleUtility.WorldPointToSizedRect (position, GUIContent.none, GUIStyle.none);
            sizedRect.size = size + Vector2.one * 2;
            sizedRect.position = sizedRect.position - size / 2f;
            GUI.Label (sizedRect, new GUIContent (text), textPointStyle);

            UnityEditor.Handles.EndGUI ();
            #endif
        }

        public static void BracketIndicator (Vector3 startPosition, Vector3 endPosition, string text, Color color, float spacing = 1, float arrowSize = 2)
        {
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

            UnityEditor.Handles.color = color;
            UnityEditor.Handles.DrawLine (startPosition, startSpacedPos);
            UnityEditor.Handles.DrawLine (endPosition, endSpacedPos);

            UnityEditor.Handles.DrawLine (startSpacedPos, startArrowPos);
            UnityEditor.Handles.DrawLine (endSpacedPos, endArrowPos);
            UnityEditor.Handles.DrawLine (startArrowPos, middleArrowPos);
            UnityEditor.Handles.DrawLine (endArrowPos, middleArrowPos);
            UnityEditor.Handles.color = Color.white;

            TextPoint (middleArrowPos, text);
            #endif
        }

        #region Point
        public static void Point (Vector3 position, Color color, float size = 1)
        {
            Color oldColor = Gizmos.color;

            Gizmos.color = color;
            Gizmos.DrawRay (position + (Vector3.up * (size * 0.5f)), -Vector3.up * size);
            Gizmos.DrawRay (position + (Vector3.right * (size * 0.5f)), -Vector3.right * size);
            Gizmos.DrawRay (position + (Vector3.forward * (size * 0.5f)), -Vector3.forward * size);

            Gizmos.color = oldColor;
        }

        public static void Point (Vector3 position, float size = 1)
        {
            Point (position, Color.white, size);
        }
        #endregion

        #region Bounds
        public static void Bounds (Bounds bounds, Color color)
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

            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            Gizmos.DrawLine (ruf, luf);
            Gizmos.DrawLine (ruf, rub);
            Gizmos.DrawLine (luf, lub);
            Gizmos.DrawLine (rub, lub);

            Gizmos.DrawLine (ruf, rdf);
            Gizmos.DrawLine (rub, rdb);
            Gizmos.DrawLine (luf, lfd);
            Gizmos.DrawLine (lub, lbd);

            Gizmos.DrawLine (rdf, lfd);
            Gizmos.DrawLine (rdf, rdb);
            Gizmos.DrawLine (lfd, lbd);
            Gizmos.DrawLine (lbd, rdb);

            Gizmos.color = oldColor;
        }

        public static void Bounds (Bounds bounds)
        {
            Bounds (bounds, Color.white);
        }
        #endregion

        #region Local Cube
        public static void LocalCube (Transform transform, Vector3 size, Color color, Vector3 center = default(Vector3))
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            Vector3 lbb = transform.TransformPoint (center + ((-size) * 0.5f));
            Vector3 rbb = transform.TransformPoint (center + (new Vector3 (size.x, -size.y, -size.z) * 0.5f));

            Vector3 lbf = transform.TransformPoint (center + (new Vector3 (size.x, -size.y, size.z) * 0.5f));
            Vector3 rbf = transform.TransformPoint (center + (new Vector3 (-size.x, -size.y, size.z) * 0.5f));

            Vector3 lub = transform.TransformPoint (center + (new Vector3 (-size.x, size.y, -size.z) * 0.5f));
            Vector3 rub = transform.TransformPoint (center + (new Vector3 (size.x, size.y, -size.z) * 0.5f));

            Vector3 luf = transform.TransformPoint (center + ((size) * 0.5f));
            Vector3 ruf = transform.TransformPoint (center + (new Vector3 (-size.x, size.y, size.z) * 0.5f));

            Gizmos.DrawLine (lbb, rbb);
            Gizmos.DrawLine (rbb, lbf);
            Gizmos.DrawLine (lbf, rbf);
            Gizmos.DrawLine (rbf, lbb);

            Gizmos.DrawLine (lub, rub);
            Gizmos.DrawLine (rub, luf);
            Gizmos.DrawLine (luf, ruf);
            Gizmos.DrawLine (ruf, lub);

            Gizmos.DrawLine (lbb, lub);
            Gizmos.DrawLine (rbb, rub);
            Gizmos.DrawLine (lbf, luf);
            Gizmos.DrawLine (rbf, ruf);

            Gizmos.color = oldColor;
        }

        public static void LocalCube (Transform transform, Vector3 size, Vector3 center = default(Vector3))
        {
            LocalCube (transform, size, Color.white, center);
        }

        public static void LocalCube (Matrix4x4 space, Vector3 size, Color color, Vector3 center = default(Vector3))
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            Vector3 lbb = space.MultiplyPoint3x4 (center + ((-size) * 0.5f));
            Vector3 rbb = space.MultiplyPoint3x4 (center + (new Vector3 (size.x, -size.y, -size.z) * 0.5f));

            Vector3 lbf = space.MultiplyPoint3x4 (center + (new Vector3 (size.x, -size.y, size.z) * 0.5f));
            Vector3 rbf = space.MultiplyPoint3x4 (center + (new Vector3 (-size.x, -size.y, size.z) * 0.5f));

            Vector3 lub = space.MultiplyPoint3x4 (center + (new Vector3 (-size.x, size.y, -size.z) * 0.5f));
            Vector3 rub = space.MultiplyPoint3x4 (center + (new Vector3 (size.x, size.y, -size.z) * 0.5f));

            Vector3 luf = space.MultiplyPoint3x4 (center + ((size) * 0.5f));
            Vector3 ruf = space.MultiplyPoint3x4 (center + (new Vector3 (-size.x, size.y, size.z) * 0.5f));

            Gizmos.DrawLine (lbb, rbb);
            Gizmos.DrawLine (rbb, lbf);
            Gizmos.DrawLine (lbf, rbf);
            Gizmos.DrawLine (rbf, lbb);

            Gizmos.DrawLine (lub, rub);
            Gizmos.DrawLine (rub, luf);
            Gizmos.DrawLine (luf, ruf);
            Gizmos.DrawLine (ruf, lub);

            Gizmos.DrawLine (lbb, lub);
            Gizmos.DrawLine (rbb, rub);
            Gizmos.DrawLine (lbf, luf);
            Gizmos.DrawLine (rbf, ruf);

            Gizmos.color = oldColor;
        }

        public static void LocalCube (Matrix4x4 space, Vector3 size, Vector3 center = default(Vector3))
        {
            LocalCube (space, size, Color.white, center);
        }
        #endregion

        #region Circle
        public static void Circle (Vector3 position, Vector3 up, Color color, float radius = 1)
        {
            up = ((up == Vector3.zero) ? Vector3.up : up).normalized * radius;
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

            Color oldColor = Gizmos.color;
            Gizmos.color = (color == default(Color)) ? Color.white : color;

            for (var i = 0; i < 91; i++)
            {
                nextPoint.x = Mathf.Cos ((i * 4) * Mathf.Deg2Rad);
                nextPoint.z = Mathf.Sin ((i * 4) * Mathf.Deg2Rad);
                nextPoint.y = 0;

                nextPoint = position + matrix.MultiplyPoint3x4 (nextPoint);

                Gizmos.DrawLine (lastPoint, nextPoint);
                lastPoint = nextPoint;
            }

            Gizmos.color = oldColor;
        }

        public static void Circle (Vector3 position, Color color, float radius = 1)
        {
            Circle (position, Vector3.up, color, radius);
        }

        public static void Circle (Vector3 position, Vector3 up, float radius = 1)
        {
            Circle (position, up, Color.white, radius);
        }

        public static void Circle (Vector3 position, float radius = 1)
        {
            Circle (position, Vector3.up, Color.white, radius);
        }
        #endregion

        #region Cylinder
        public static void Cylinder (Vector3 start, Vector3 end, Color color, float radius = 1)
        {
            Vector3 up = (end - start).normalized * radius;
            Vector3 forward = Vector3.Slerp (up, -up, 0.5f);
            Vector3 right = Vector3.Cross (up, forward).normalized * radius;

            //Radial circles
            Circle (start, up, color, radius);
            Circle (end, -up, color, radius);
            Circle ((start + end) * 0.5f, up, color, radius);

            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            //Side lines
            Gizmos.DrawLine (start + right, end + right);
            Gizmos.DrawLine (start - right, end - right);

            Gizmos.DrawLine (start + forward, end + forward);
            Gizmos.DrawLine (start - forward, end - forward);

            //Start endcap
            Gizmos.DrawLine (start - right, start + right);
            Gizmos.DrawLine (start - forward, start + forward);

            //End endcap
            Gizmos.DrawLine (end - right, end + right);
            Gizmos.DrawLine (end - forward, end + forward);

            Gizmos.color = oldColor;
        }

        public static void Cylinder (Vector3 start, Vector3 end, float radius = 1)
        {
            Cylinder (start, end, Color.white, radius);
        }
        #endregion

        #region Cone
        public static void Cone (Vector3 position, Vector3 direction, Color color, float angle = 45)
        {
            float length = direction.magnitude;

            Vector3 forward = direction;
            Vector3 up = Vector3.Slerp (forward, -forward, 0.5f);
            Vector3 right = Vector3.Cross (forward, up).normalized * length;

            direction = direction.normalized;

            Vector3 slerpedVector = Vector3.Slerp (forward, up, angle / 90.0f);

            float distance;
            var farPlane = new Plane (-direction, position + forward);
            var distRay = new Ray (position, slerpedVector);

            farPlane.Raycast (distRay, out distance);

            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            Gizmos.DrawRay (position, slerpedVector.normalized * distance);
            Gizmos.DrawRay (position, Vector3.Slerp (forward, -up, angle / 90).normalized * distance);
            Gizmos.DrawRay (position, Vector3.Slerp (forward, right, angle / 90).normalized * distance);
            Gizmos.DrawRay (position, Vector3.Slerp (forward, -right, angle / 90).normalized * distance);

            Circle (position + forward, direction, color, (forward - (slerpedVector.normalized * distance)).magnitude);
            Circle (position + (forward * 0.5f), direction, color, ((forward * 0.5f) - (slerpedVector.normalized * (distance * 0.5f))).magnitude);

            Gizmos.color = oldColor;
        }

        public static void Cone (Vector3 position, Vector3 direction, float angle = 45)
        {
            Cone (position, direction, Color.white, angle);
        }

        public static void Cone (Vector3 position, Color color, float angle = 45)
        {
            Cone (position, Vector3.up, color, angle);
        }

        public static void Cone (Vector3 position, float angle = 45)
        {
            Cone (position, Vector3.up, Color.white, angle);
        }
        #endregion

        #region Arrow
        public static void Arrow (Vector3 position, Vector3 direction, Color color)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            Gizmos.DrawRay (position, direction);
            Cone (position + direction, -direction * 0.333f, color, 15);

            Gizmos.color = oldColor;
        }

        public static void Arrow (Vector3 position, Vector3 direction)
        {
            Arrow (position, direction, Color.white);
        }
        #endregion

        #region Capsule
        public static void Capsule (Vector3 start, Vector3 end, Color color, float radius = 1)
        {
            Vector3 up = (end - start).normalized * radius;
            Vector3 forward = Vector3.Slerp (up, -up, 0.5f);
            Vector3 right = Vector3.Cross (up, forward).normalized * radius;

            Color oldColor = Gizmos.color;
            Gizmos.color = color;

            float height = (start - end).magnitude;
            float sideLength = Mathf.Max (0, (height * 0.5f) - radius);
            Vector3 middle = (end + start) * 0.5f;

            start = middle + ((start - middle).normalized * sideLength);
            end = middle + ((end - middle).normalized * sideLength);

            //Radial circles
            Circle (start, up, color, radius);
            Circle (end, -up, color, radius);

            //Side lines
            Gizmos.DrawLine (start + right, end + right);
            Gizmos.DrawLine (start - right, end - right);

            Gizmos.DrawLine (start + forward, end + forward);
            Gizmos.DrawLine (start - forward, end - forward);

            for (int i = 1; i < 26; i++)
            {
                //Start endcap
                Gizmos.DrawLine (Vector3.Slerp (right, -up, i / 25) + start, Vector3.Slerp (right, -up, (i - 1) / 25) + start);
                Gizmos.DrawLine (Vector3.Slerp (-right, -up, i / 25) + start, Vector3.Slerp (-right, -up, (i - 1) / 25) + start);
                Gizmos.DrawLine (Vector3.Slerp (forward, -up, i / 25) + start, Vector3.Slerp (forward, -up, (i - 1) / 25) + start);
                Gizmos.DrawLine (Vector3.Slerp (-forward, -up, i / 25) + start, Vector3.Slerp (-forward, -up, (i - 1) / 25) + start);

                //End endcap
                Gizmos.DrawLine (Vector3.Slerp (right, up, i / 25) + end, Vector3.Slerp (right, up, (i - 1) / 25) + end);
                Gizmos.DrawLine (Vector3.Slerp (-right, up, i / 25) + end, Vector3.Slerp (-right, up, (i - 1) / 25) + end);
                Gizmos.DrawLine (Vector3.Slerp (forward, up, i / 25) + end, Vector3.Slerp (forward, up, (i - 1) / 25) + end);
                Gizmos.DrawLine (Vector3.Slerp (-forward, up, i / 25) + end, Vector3.Slerp (-forward, up, (i - 1) / 25) + end);
            }

            Gizmos.color = oldColor;
        }

        public static void Capsule (Vector3 start, Vector3 end, float radius = 1)
        {
            Capsule (start, end, Color.white, radius);
        }
        #endregion
        #endregion
    }
}