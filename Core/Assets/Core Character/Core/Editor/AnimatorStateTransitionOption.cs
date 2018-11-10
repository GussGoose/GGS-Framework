using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorStateTransitionOption {

	#region Class members
	#endregion

	#region Class accesors
	#endregion

	#region Class overrides
	#endregion

	#region Class implementation
	[MenuItem ("CONTEXT/AnimatorStateTransition/Setup")]
	private static void Setup () {
		AnimatorStateTransition transition = Selection.activeObject as AnimatorStateTransition;
		transition.hasExitTime = false;
		transition.exitTime = 1;
		transition.hasFixedDuration = true;
		transition.duration = 0.05f;
		transition.offset = 0;
		transition.interruptionSource = TransitionInterruptionSource.None;
	}
	#endregion

	#region Interface implementation
	#endregion
}