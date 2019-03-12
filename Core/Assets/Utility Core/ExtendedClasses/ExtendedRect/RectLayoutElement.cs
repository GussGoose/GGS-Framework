using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectLayoutElement {

	#region Class members
	public bool use;
	public string key;
	public float size;
	public bool expand;
	#endregion

	#region Class overrides
	#endregion

	#region Class implementation
	/// <summary>
	/// Makes a rect with custom size.
	/// </summary>
	/// <param name="size">Custom size.</param>
	/// <param name="use">If it is true, the rect will be used, otherwise it will be ignored. By default it is used.</param>
	public RectLayoutElement (string elementKey, float size, bool use = true) {
		this.use = use;
		key = elementKey;
		this.size = size;
	}

	/// <summary>
	/// Makes a rect with expanded size.
	/// </summary>
	/// <param name="use">If it is true, the rect will be used, otherwise it will be ignored. By default it is used.</param>
	public RectLayoutElement (string elementKey, bool use = true) {
		this.use = use;
		key = elementKey;
		size = 0;
		expand = true;
	}

	/// <summary>
	/// Makes a custom spacing between rects.
	/// </summary>
	/// <param name="use">If it is true, the rect will be used, otherwise it will be ignored. By default it is used.</param>
	public RectLayoutElement (float spacing, bool use = true) {
		this.use = use;
		key = string.Empty;
		size = spacing;
	}
	#endregion

	#region Interface implementation
	#endregion
}