using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework.Editor
{
	[Serializable]
	public class ReorderableListState
	{
		#region Class Members
		[SerializeField] private TreeViewState treeViewState;
		[SerializeField] private int uniqueId;
		#endregion

		#region Class Accesors
		public TreeViewState TreeViewState
		{
			get { return treeViewState; }
		}

		public int UniqueId
		{
			get
			{
				if (uniqueId == 0)
					uniqueId = EditorGUIUtility.GetControlID (FocusType.Passive) * 1000;

				return uniqueId;
			}
		}
		#endregion

		#region Class Implementation
		public ReorderableListState ()
		{
			treeViewState = new TreeViewState ();
		}
		#endregion
	}
}