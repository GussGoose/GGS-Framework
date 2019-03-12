using UnityEngine;

public static partial class ExtensionMethods {

	#region Class members
	public static Rect Expand (this Rect rect, float ammount) {
		Vector2 pos = rect.position + Vector2.one * ammount;
		Vector2 size = rect.size - Vector2.one * ammount * 2;
		Rect newRect = new Rect (pos, size);
		return newRect;
	}

	public static Rect Expand (this Rect rect, float ammount, Vector2 dir) {
		Vector2 pos = rect.position + dir * ammount;
		Vector2 size = rect.size - dir * ammount * 2;
		Rect newRect = new Rect (pos, size);
		return newRect;
	}

	public static Rect Expand (this Rect rect, float ammount, Vector2 dir, float offset) {
		Vector2 pos = rect.position + dir * ammount - offset * dir;
		Vector2 size = rect.size - dir * ammount * 2;
		Rect newRect = new Rect (pos, size);
		return newRect;
	}

	public static Rect HorizontalExpand (this Rect rect, float ammount) {
		Vector2 pos = rect.position + Vector2.right * ammount;
		Vector2 size = rect.size - Vector2.right * ammount * 2;
		Rect newRect = new Rect (pos, size);
		return newRect;
	}

	public static Rect VerticalExpand (this Rect rect, float ammount) {
		Vector2 pos = rect.position + Vector2.up * ammount;
		Vector2 size = rect.size - Vector2.up * ammount * 2;
		Rect newRect = new Rect (pos, size);
		return newRect;
	}
	#endregion
}