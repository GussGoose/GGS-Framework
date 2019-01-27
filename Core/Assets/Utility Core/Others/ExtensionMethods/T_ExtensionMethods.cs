using UnityEngine;
using System.Collections;
using UnityEditor.Animations;
using System.Collections.Generic;

public static partial class ExtensionMethods {

	#region Class implementation
	public static T CloneObject<T> (this T obj) where T : class {
		if (obj == null) return null;
		System.Reflection.MethodInfo instance = obj.GetType ().GetMethod ("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
		if (instance != null)
			return (T) instance.Invoke (obj, null);
		else
			return null;
	}
	#endregion

	#region AnimatorStateMachine
#if UNITY_EDITOR
	public static List<ChildAnimatorState> GetAllStates (this AnimatorStateMachine animatorStateMachine) {
		ChildAnimatorStateMachine[] stateMachines = animatorStateMachine.stateMachines;
		ChildAnimatorState[] states = animatorStateMachine.states;

		List<ChildAnimatorState> childAnimatorStateList = new List<ChildAnimatorState> ();
		childAnimatorStateList.AddRange (states);

		foreach (ChildAnimatorStateMachine stateMachine in stateMachines)
			childAnimatorStateList.AddRange (stateMachine.stateMachine.GetAllStates ());

		return childAnimatorStateList;
	}

	public static List<ChildAnimatorStateMachine> GetAllSubStateMachines (this AnimatorStateMachine animatorStateMachine) {
		ChildAnimatorStateMachine[] stateMachines = animatorStateMachine.stateMachines;

		List<ChildAnimatorStateMachine> animatorStateMachineList = new List<ChildAnimatorStateMachine> ();
		animatorStateMachineList.AddRange (stateMachines);

		foreach (ChildAnimatorStateMachine stateMachine in stateMachines)
			animatorStateMachineList.AddRange (stateMachine.stateMachine.GetAllSubStateMachines ());

		return animatorStateMachineList;
	}
#endif
	#endregion
}