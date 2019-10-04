using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GGS_Framework
{
	public class TestsEditorWindow : EditorWindow
	{
		#region Class Members
		private UnityEditorInternal.ReorderableList reorderableList;
		#endregion

		#region Class Accesors
		private Tests Target
		{
			get { return FindObjectOfType<Tests> (); }
		}
		#endregion

		#region Class Overrides
		private void OnEnable ()
		{
			SerializedObject serializedObject = new SerializedObject (Target);
			reorderableList = new UnityEditorInternal.ReorderableList (serializedObject, serializedObject.FindProperty ("list"));

			reorderableList.onAddDropdownCallback += OnAddDropdown;
			reorderableList.onAddCallback += OnAdd;

			reorderableList.drawElementCallback += OnElementDraw;
		}

		private void OnGUI ()
		{
			reorderableList.DoLayoutList ();
		}
		#endregion

		#region Class Implementation
		[MenuItem ("Window/GGS Framework/Development/Tests")]
		public static void Open ()
		{
			TestsEditorWindow window = GetWindow<TestsEditorWindow> ();
			window.titleContent = new GUIContent ("GGSF Tests");
			window.Show ();
		}

		private void OnAdd (ReorderableList list)
		{

			//list.serializedProperty.GetArrayElementAtIndex (list.index).intValue = 10;
		}

		private void OnAddDropdown (Rect buttonRect, ReorderableList list)
		{
			//EditorGUI.DrawRect (buttonRect, Color.red);
			//throw new NotImplementedException ();
		}

		private void OnElementDraw (Rect rect, int index, bool isActive, bool isFocused)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}
