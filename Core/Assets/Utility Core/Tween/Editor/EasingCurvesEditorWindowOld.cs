using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class EasingCurvesEditorWindowOld : EditorWindow {

	#region Class members
	private const float delay = 0.2f;

	private float resolution;

	private float scrollPosition;
	private Vector2 spacing;
	private float padding;

	private float yOffset;
	private Vector2 curveSize;

	private float currentTime;

	private int prevSelectedCurve;
	private Rect selectedRect;
	private EasingCurves.List selectedEase;

	private Vector2Int tableSize;
	#endregion

	#region Class overrides
	[MenuItem ("Tools/Tween/Easing Curves Old")]
	static void Init () {
		EasingCurvesEditorWindowOld window = (EasingCurvesEditorWindowOld) GetWindow (typeof (EasingCurvesEditorWindowOld));
		window.Show ();
	}

	private void OnEnable () {
		curveSize = new Vector2 (175, 100);
		spacing = new Vector2 (30, 10);
		padding = 10;

		yOffset = 80;
		resolution = 30;

		EditorApplication.update += delegate { Repaint (); };
	}

	private void OnGUI () {
		float screenWidth = position.width - padding * 2 + spacing.x;
		int horizontalCount = (int) (screenWidth / (curveSize.x + 30));
		int easingCount = Enum.GetNames (typeof (EasingCurves.List)).Length;

		tableSize = new Vector2Int (horizontalCount, Mathf.CeilToInt (easingCount / horizontalCount) + 1);

		Rect scrollBarRect = ExtendedRect.HorizontalRects (new Rect (Vector2.zero, position.size),
			new RectLayoutElement ("Expanded"),
			new RectLayoutElement ("ScrollBarRect", 18))["ScrollBarRect"];

		float scrollBarSize = tableSize.y * (curveSize.y + yOffset) + (tableSize.y - 1) * spacing.y + padding * 2;

		if (scrollBarSize > position.height) {
			if (position.Contains (Event.current.mousePosition)) {
				if (Event.current.type == EventType.ScrollWheel) {
					scrollPosition += Event.current.delta.y * 50;
					HandleUtility.Repaint ();
				}
			}

			scrollPosition = GUI.VerticalScrollbar (scrollBarRect, scrollPosition, position.height / 2f, 0, scrollBarSize - position.height / 2f);
		}
		else
			scrollPosition = 0;

		bool drawIndicator = false;
		Rect[,] rects = GridRects (tableSize, new Vector2 (curveSize.x, curveSize.y + yOffset), spacing);
		int index = 0;
		for (int y = 0; y < tableSize.y; y++) {
			for (int x = 0; x < tableSize.x; x++) {
				if (index < Enum.GetNames (typeof (EasingCurves.List)).Length) {
					Rect thisRect = rects[x, y];
					thisRect.position += padding * Vector2.one;
					thisRect.y -= scrollPosition;

					DrawEasing (thisRect, (EasingCurves.List) index);

					if (thisRect.Contains (Event.current.mousePosition)) {
						drawIndicator = true;

						if (prevSelectedCurve != index) {
							prevSelectedCurve = index;
							CurveSelected (thisRect, (EasingCurves.List) index);
						}
					}
					index += 1;
				}
			}
		}

		if (drawIndicator)
			DrawIndicator (selectedRect, selectedEase);
	}
	#endregion

	#region Class implementation
	private void CurveSelected (Rect rect, EasingCurves.List ease) {
		currentTime = Time.realtimeSinceStartup;
		selectedRect = rect;
		selectedEase = ease;
	}

	private void DrawEasing (Rect rect, EasingCurves.List ease) {
		Vector2 size = curveSize;
		size.x -= 2;

		Vector2 offset = new Vector2 (1, rect.height - (rect.height - size.y) / 2f);

		Vector2 bottomLeft = offset;
		Vector2 bottomRight = offset + size.x * Vector2.right;
		Vector2 topLeft = offset - size.y * Vector2.up;
		Vector2 topRigth = new Vector2 (offset.x + size.x, offset.y - size.y);

		GUI.Box (rect, "", EditorStyles.helpBox);
		GUILayout.BeginArea (rect);
		Handles.color = new Color (0, 1, 0, 0.5f);
		Handles.DrawLine (bottomLeft, bottomRight);

		Handles.color = new Color (1, 0, 0, 0.5f);
		Handles.DrawLine (topLeft, topRigth);

		for (int i = 1; i < resolution + 1; i++) {
			float prevPercent = (i > 0 ? i - 1 : i) / resolution;
			float percent = i / resolution;
			Vector2 prevPos = new Vector2 (prevPercent * size.x + offset.x, -EasingCurves.GetEaseValue (ease, prevPercent) * size.y + offset.y);
			Vector2 pos = new Vector2 (percent * size.x + offset.x, -EasingCurves.GetEaseValue (ease, percent) * size.y + offset.y);

			Handles.color = Color.white;
			Handles.DrawLine (prevPos, pos);
		}
		GUILayout.EndArea ();

		Dictionary<string, Rect> rects = ExtendedRect.VerticalRects (rect,
			new RectLayoutElement ("Header", yOffset / 3f));

		ExtendedGUI.DrawTitle (rects["Header"].Expand (4), ease.ToString ().ToTitleCase (), EditorStyles.helpBox, FontStyle.Bold);
	}

	private void DrawIndicator (Rect rect, EasingCurves.List ease) {
		float time = Mathf.Clamp (Time.realtimeSinceStartup - currentTime, 0, 1 + delay);

		Color curvePointColor = Color.green;
		Color indicatorPointColor = Color.white;
		if (time < delay) {
			float alpha = 1f / delay / 2f * time;
			curvePointColor.a = alpha;
			indicatorPointColor.a = alpha;
			time = 0;
		}
		else
			time -= delay;

		Vector2 size = curveSize;
		size.x -= 2;

		Vector2 offset = new Vector2 (1, rect.height - (rect.height - size.y) / 2f);

		Vector2 curvePoint = new Vector2 (time * size.x + offset.x, -EasingCurves.GetEaseValue (ease, time) * size.y + offset.y);
		Handles.color = curvePointColor;
		Handles.DrawSolidDisc (rect.position + curvePoint, Vector3.back, 4);

		Vector2 indicatorPoint = new Vector2 (offset.x + size.x, curvePoint.y);
		Handles.color = indicatorPointColor;

		GUIStyle style = new GUIStyle ("ChannelStripAttenuationMarkerSquare");
		Vector2 styleOffset = new Vector2 (-1, style.CalcHeight (new GUIContent (), 0) / 2f);
		if (Event.current.type == EventType.Repaint) {
			GUI.color = indicatorPointColor;
			style.Draw (new Rect (rect.position + indicatorPoint - styleOffset, Vector2.one * 20), new GUIContent (), 0);
		}
		GUI.color = Color.white;
	}

	private Rect[,] GridRects (Vector2Int size, Vector2 cellSize, Vector2 cellSpacing) {
		Rect[,] rects = new Rect[size.x, size.y];

		for (int x = 0; x < size.x; x++) {
			for (int y = 0; y < size.y; y++) {
				Vector2 pos = new Vector2 (cellSize.x * x + cellSpacing.x * x, cellSize.y * y + cellSpacing.y * y);
				rects[x, y] = new Rect (pos, cellSize);
			}
		}

		return rects;
	}
	#endregion

	#region Interface implementation
	#endregion
}