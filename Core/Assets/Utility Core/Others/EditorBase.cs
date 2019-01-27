using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class EditorBase<T> : Editor where T : class {

	#region Class members
	public new T targetClass;
	public SerializedObject serializedObject;
	#endregion

	#region Class overrides
	public virtual void OnEnable () {
		targetClass = base.target as T;
		serializedObject = new SerializedObject (base.target);
	}
	#endregion
}