using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class CustomReorderableList {

	#region Class members
	public ReorderableList.ElementCallbackDelegate drawElementCallback;

	public UnityEventT<int> onElementRemoved = new UnityEventT<int> ();
	public UnityEventT<int> onElementAdded = new UnityEventT<int> ();
	public UnityEventT<int> onElementInserted = new UnityEventT<int> ();
	public UnityEventT<int> onElementDuplicated = new UnityEventT<int> ();
	private System.Type elementType;

	public Rect Rect { get; private set; }

	#region Default members
	// Title
	private FontStyle defaultHeaderFontStyle = FontStyle.Bold;

	// Element
	private float defaultElementHeight = 23;
	private float[] elementHeights = new float[0];

	// Header and Footer
	private float defaultHeaderHeight = 18;
	private float defaultFooterHeight = 18;

	private string defaultHeaderStyle;
	private string defaultBackgroundStyle;
	#endregion

	#region Custom members
	// Title
	private bool displayHeader;
	private bool displayBackground;
	private bool displayFooter;
	private bool displayRemove;
	public bool removeConfirmation;
	public string headerTitle;
	public float headerHeight;
	public FontStyle headerFontStyle;

	public float footerHeight;

	// Styles
	public string headerStyle;
	public string backgroundStyle;
	#endregion
	#endregion

	#region Class accesors
	public ReorderableList ReorderableList { get; private set; }

	public IList List {
		get { return ReorderableList.list; }
		set { ReorderableList.list = value; }
	}

	public bool Draggable {
		get { return ReorderableList.draggable; }
		set { ReorderableList.draggable = value; }
	}

	public bool UsingScrollBar { get; private set; }

	public float ElementHeight {
		get { return ReorderableList.elementHeight; }
		set { ReorderableList.elementHeight = value; }
	}

	public float ListHeight {
		get {
			float height = 0;
			height += TotalHeaderHeight;
			if (displayFooter)
				height += footerHeight;
			height += ReorderableList.count * ReorderableList.elementHeight;
			return height;
		}
	}

	public float ScrollPosition {
		get { return EditorPrefs.GetFloat ("CustomReorderableListScrollPosition" + headerTitle); }
		set { EditorPrefs.SetFloat ("CustomReorderableListScrollPosition" + headerTitle, value); }
	}

	public bool AutomaticAdd { get; set; }
	public bool AutomaticRemove { get; set; }
	public bool AutomaticInsert { get; set; }
	public bool AutomaticDuplicate { get; set; }

	public float TotalHeaderHeight {
		get {
			float headerHeight = 0;

			if (displayHeader)
				headerHeight += this.headerHeight;
			if (DrawColumnIndicators)
				headerHeight += this.headerHeight;

			return headerHeight;
		}
	}

	public bool DrawColumnIndicators { get { return columnLayoutElements.Count > 0; } }
	public List<RectLayoutElement> columnLayoutElements = new List<RectLayoutElement> ();
	#endregion

	#region Class implementation
	public CustomReorderableList (IList list, System.Type elementType, bool draggable = true, bool displayHeader = true, bool displayBackground = true, bool displayFooter = true, bool displayRemove = true, string headerTitle = "Reorderable List") {
		ReorderableList = new ReorderableList (list, elementType, draggable, false, false, false);
		ReorderableList.headerHeight = 0;
		ReorderableList.footerHeight = 0;
		ReorderableList.showDefaultBackground = false;

		defaultHeaderStyle = "RL Header";
		defaultBackgroundStyle = "RL Background";
		InitList (elementType, draggable, displayHeader, defaultHeaderHeight, headerTitle, defaultHeaderStyle, defaultHeaderFontStyle, displayBackground, defaultBackgroundStyle, defaultElementHeight, displayFooter, displayRemove, defaultFooterHeight);

		ReorderableList.drawElementCallback = OnDrawElement;
		ReorderableList.elementHeightCallback = OnElementHeight;
	}

	private void InitList (System.Type elementType, bool draggable, bool displayHeader, float headerHeight, string headerTitle, string headerStyle, FontStyle headerFontStyle, bool displayBackground, string backgroundStyle, float elementHeight, bool displayFooter, bool displayRemove, float footerHeight) {
		this.elementType = elementType;
		Draggable = draggable;

		this.displayHeader = displayHeader;
		this.headerHeight = headerHeight;
		this.headerTitle = headerTitle;
		this.headerStyle = headerStyle;
		this.headerFontStyle = headerFontStyle;

		this.displayBackground = displayBackground;
		this.backgroundStyle = backgroundStyle;
		this.ElementHeight = elementHeight;

		this.displayFooter = displayFooter;
		this.displayRemove = displayRemove;
		this.footerHeight = footerHeight;

		AutomaticAdd = true;
		AutomaticRemove = true;
	}

	public void Draw () {
		if (elementHeights.Length != List.Count)
			SetupElementHeights ();

		Rect rect = GUILayoutUtility.GetRect (0, ListHeight, GUILayout.ExpandWidth (true));
		if (Event.current.type == EventType.Repaint)
			Rect = rect;

		Draw (Rect);
	}

	public void Draw (Rect rect) {
		GUILayout.BeginArea (rect);
		rect.position = Vector2.zero;

		if (elementHeights.Length != List.Count)
			SetupElementHeights ();

		Dictionary<string, Rect> verticalRects = ExtendedRect.VerticalRects (rect,
			new RectLayoutElement ("Header", TotalHeaderHeight, displayHeader || DrawColumnIndicators),
			new RectLayoutElement ("Content"),
			new RectLayoutElement ("Footer", footerHeight, displayFooter));

		if (ReorderableList.list.Count * ReorderableList.elementHeight > verticalRects["Content"].height)
			UsingScrollBar = true;
		else
			UsingScrollBar = false;

		Dictionary<string, Rect> horizontalRects = ExtendedRect.HorizontalRects (verticalRects["Content"],
			new RectLayoutElement ("List"),
			new RectLayoutElement ("ScrollBar", 16, UsingScrollBar));

		// Draw header
		DrawHeader (verticalRects["Header"]);

		// Draw list
		if (displayBackground) {
			GUIStyle backgroundStyle = new GUIStyle (this.backgroundStyle);
			backgroundStyle.fixedHeight = verticalRects["Content"].height + 2;
			backgroundStyle.Draw (verticalRects["Content"]);
		}

		ScrollPosition = (!UsingScrollBar) ? 0 : GUI.VerticalScrollbar (horizontalRects["ScrollBar"], ScrollPosition, horizontalRects["ScrollBar"].height, 0, ReorderableList.list.Count * ReorderableList.elementHeight);
		Event currentEvent = Event.current;

		if (verticalRects["Content"].Contains (currentEvent.mousePosition)) {
			if (currentEvent.type == EventType.ScrollWheel) {
				ScrollPosition += currentEvent.delta.y * 50;
				HandleUtility.Repaint ();
			}
		}

		Rect listRect = horizontalRects["List"];
		listRect.y = 0;
		listRect.y -= ScrollPosition;

		GUILayout.BeginArea (verticalRects["Content"]);
		ReorderableList.DoList (listRect);
		GUILayout.EndArea ();

		// Draw Footer
		if (displayFooter)
			DrawFooter (verticalRects["Footer"]);

		GUILayout.EndArea ();
	}

	public void SetElementHeight (int index, float height) {
		SetupElementHeights ();
		elementHeights[index] = height;
	}

	public void SetDefaultElementHeight (int index) {
		elementHeights[index] = ElementHeight;
	}

	public float GetElementHeight (int index) {
		return elementHeights[index];
	}

	private void SetupElementHeights () {
		if (elementHeights.Length == List.Count)
			return;

		elementHeights = new float[List.Count];

		for (int i = 0; i < elementHeights.Length; i++)
			elementHeights[i] = ElementHeight;
	}

	private float OnElementHeight (int index) {
		SetupElementHeights ();

		if (index <= List.Count - 1)
			return elementHeights[index];

		return defaultElementHeight;
	}

	private void OnDrawElement (Rect rect, int index, bool isActive, bool isFocused) {
		Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
			new RectLayoutElement ("UsableRect"),
			new RectLayoutElement (2),
			new RectLayoutElement ("RemoveButton", 18, displayRemove));

		if (displayRemove)
			DrawRemoveButton (rects["RemoveButton"], index);

		Rect actionRect = rect;
		actionRect.x -= 15;
		actionRect.width += 15;

		Event current = Event.current;
		if (actionRect.Contains (current.mousePosition) && current.type == EventType.ContextClick) {
			CustomGenericMenu.Draw ((option) => {
				if (option == "Insert Above") {
					if (AutomaticInsert)
						List.InsertAbove (index);
					onElementInserted.Invoke (index);
				}
				if (option == "Insert Below") {
					if (AutomaticInsert)
						List.InsertBelow (index);
					onElementInserted.Invoke (index + 1);
				}
				if (option == "Duplicate") {
					if (AutomaticDuplicate)
						List.Duplicate (index);
					onElementDuplicated.Invoke (index);
				}
			}, "Insert Above", "Insert Below", "Duplicate");

			current.Use ();
		}

		if (index != ReorderableList.list.Count) {
			if (List.Count > 0) {
				if (drawElementCallback != null)
					drawElementCallback.Invoke (rects["UsableRect"].Expand (2), index, isActive, isFocused);
			}
		}
	}

	private void DrawHeader (Rect rect) {
		Dictionary<string, Rect> vRects = ExtendedRect.VerticalRects (rect,
			new RectLayoutElement ("Header", headerHeight, displayHeader),
			new RectLayoutElement ("Indicators", headerHeight, DrawColumnIndicators));

		GUIStyle indicatorStyle = new GUIStyle ("RL Header");
		indicatorStyle.fixedHeight = TotalHeaderHeight + 1;
		indicatorStyle.Draw (rect);
		indicatorStyle.fixedHeight = 0;

		if (displayHeader)
			ExtendedGUI.DrawTitle (vRects["Header"], headerTitle, headerFontStyle);

		if (!DrawColumnIndicators)
			return;

		List<RectLayoutElement> layoutElements = new List<RectLayoutElement> ();
		layoutElements.Insert (0, new RectLayoutElement (21));
		layoutElements.AddRange (columnLayoutElements);
		layoutElements.Add (new RectLayoutElement (UsingScrollBar ? 45 : 28));
		Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (vRects["Indicators"], layoutElements.ToArray ());

		GUI.backgroundColor = new Color (0.8f, 0.8f, 0.8f, 1);
		foreach (RectLayoutElement layoutElement in columnLayoutElements)
			ExtendedGUI.DrawTitle (rects[layoutElement.key], layoutElement.key, indicatorStyle);
		GUI.backgroundColor = Color.white;
	}

	private void DrawRemoveButton (Rect rect, int index) {
		Texture2D buttonTexture = EditorGUIUtility.Load ("CustomReorderableList/RemoveButton.png") as Texture2D;
		GUIStyle buttonStyle = new GUIStyle (EditorStyles.label);
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		if (GUI.Button (rect, buttonTexture, buttonStyle)) {
			if (removeConfirmation) {
				if (EditorUtility.DisplayDialog ("Remove Element?", "Are you sure you want remove this element?", "Remove", "Don't Remove"))
					RemoveElement (index);
			}
			else
				RemoveElement (index);
		}
	}

	private void DrawFooter (Rect rect) {
		GUI.backgroundColor = Color.green;
		if (GUI.Button (rect, "Add")) {
			if (AutomaticAdd)
				List.Add (ExtensionMethods.GetElementTypeOfList (List));
			onElementAdded.Invoke (List.Count);
		}
		GUI.backgroundColor = Color.white;
	}

	private void RemoveElement (int index) {
		if (AutomaticRemove)
			List.RemoveAt (index);
		onElementRemoved.Invoke (index);
	}
	#endregion
}