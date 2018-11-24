using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Reflection;
using UnityEditor.Animations;
using System.Collections.Generic;
using System.Linq;

public class AnimatorConditionCopier : EditorWindow {

	#region Class members
	private AnimatorStateTransition currentSelection;
	private AnimatorStateTransition transitionToCopy;
	private int conditionIndexToCopy;
	#endregion

	#region Class overrides
	private void OnGUI () {
		if (Selection.activeObject != null)
			currentSelection = Selection.activeObject as AnimatorStateTransition;

		DrawTransitionConditions ();
		GUILayout.Space (8);

		Object[] selection = Selection.objects;
		if (GUILayout.Button ("Add copied condition to " + selection.Length + " Transitions")) {
			if (selection.Length != 0) {
				if (!EditorUtility.DisplayDialog ("Add Condition", "You are sure you want to add this condition to the selection?", "Yes, Im sure"))
					return;

				for (int i = 0; i < selection.Length; i++) {
					AnimatorStateTransition transition = selection[i] as AnimatorStateTransition;
					List<AnimatorCondition> conditions = transition.conditions.ToList ();
					conditions.Add (transitionToCopy.conditions[conditionIndexToCopy]);
					transition.conditions = conditions.ToArray ();
				}
			}
		}
	}
	#endregion

	#region Class implementation
	[MenuItem ("Tools/Animator Copier")]
	public static void GetWindow () {
		EditorWindow window = GetWindow (typeof (AnimatorConditionCopier));
		window.titleContent = new GUIContent ("Transition Copier");
	}

	private void DrawTransitionConditions () {
		if (currentSelection == null)
			return;

		ExtendedGUI.DrawTitle ("Conditions of current selection", EditorStyles.helpBox, FontStyle.Bold);

		EditorGUILayout.BeginVertical (EditorStyles.helpBox);

		AnimatorCondition[] conditions = currentSelection.conditions;
		for (int i = 0; i < conditions.Length; i++) {
			AnimatorCondition condition = conditions[i];

			if (conditionIndexToCopy == i)
				GUI.backgroundColor = Color.green;

			if (GUILayout.Button (condition.parameter)) {
				transitionToCopy = currentSelection;
				conditionIndexToCopy = i;
			}

			GUI.backgroundColor = Color.white;
		}

		EditorGUILayout.EndVertical ();
	}
	#endregion

	#region Interface implementation
	#endregion
}