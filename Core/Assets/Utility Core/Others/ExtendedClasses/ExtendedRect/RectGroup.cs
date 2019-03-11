using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectGroup {
	#region Class members
	public RectSettings settings;

	public RectGroupOrientation orientation;
	public RectGroup[] groups;
	public RectLayoutElement[] elements = new RectLayoutElement[0];
	#endregion

	#region Class implementation
	public RectGroup (RectGroupOrientation orientation, float size, params RectGroup[] groups) {
		this.orientation = orientation;
		this.groups = groups;

		settings = new RectSettings (size);
	}

	public RectGroup (RectGroupOrientation orientation, params RectGroup[] groups) {
		this.orientation = orientation;
		this.groups = groups;

		settings = new RectSettings (0);
	}

	public RectGroup (RectGroupOrientation orientation, float size, params RectLayoutElement[] elements) {
		this.orientation = orientation;
		this.elements = elements;

		settings = new RectSettings (size);
	}

	public RectGroup (RectGroupOrientation orientation, params RectLayoutElement[] elements) {
		this.orientation = orientation;
		this.elements = elements;

		settings = new RectSettings (0);
	}

	public RectGroup (RectGroupOrientation orientation) {
		this.orientation = orientation;
		settings = new RectSettings (0);
	}
	#endregion
}