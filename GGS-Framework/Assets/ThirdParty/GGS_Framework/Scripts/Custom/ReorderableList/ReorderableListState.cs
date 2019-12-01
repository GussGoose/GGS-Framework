#if UNITY_EDITOR
using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework
{
	[Serializable]
	public class ReorderableListState
	{
		#region Class Members
		[SerializeField] private TreeViewState treeViewState;
		#endregion

		#region Class Accesors
		public TreeViewState TreeViewState
		{
			get { return treeViewState; }
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
#endif