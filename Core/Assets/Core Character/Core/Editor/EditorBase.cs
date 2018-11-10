using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class EditorBase<T> : Editor where T : class {

	#region Class members
	public new T target;
	#endregion

	#region Class overrides
	private void OnEnable () {
		target = base.target as T;
	}
	#endregion
}