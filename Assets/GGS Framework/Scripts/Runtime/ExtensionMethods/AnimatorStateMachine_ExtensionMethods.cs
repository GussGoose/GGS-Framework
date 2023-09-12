// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor.Animations;

namespace GGS_Framework
{
	public static partial class ExtensionMethods
	{
		#region Class Implementation
		#region AnimatorStateMachine
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
		#endregion 
		#endregion
	}
}
#endif