using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace GGS_Framework
{
	public static partial class ExtensionMethods
	{
		#region AnimatorStateMachine
#if UNITY_EDITOR
		public static List<ChildAnimatorState> GetAllStates (this AnimatorStateMachine animatorStateMachine)
		{
			ChildAnimatorStateMachine[] stateMachines = animatorStateMachine.stateMachines;
			ChildAnimatorState[] states = animatorStateMachine.states;

			List<ChildAnimatorState> childAnimatorStateList = new List<ChildAnimatorState> ();
			childAnimatorStateList.AddRange (states);

			foreach (ChildAnimatorStateMachine stateMachine in stateMachines)
				childAnimatorStateList.AddRange (stateMachine.stateMachine.GetAllStates ());

			return childAnimatorStateList;
		}

		public static List<ChildAnimatorStateMachine> GetAllSubStateMachines (this AnimatorStateMachine animatorStateMachine)
		{
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
}