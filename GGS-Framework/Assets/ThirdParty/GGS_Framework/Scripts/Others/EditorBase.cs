#if UNITY_EDITOR
using UnityEditor;

namespace GGS_Framework
{
	public class EditorBase<T> : Editor where T : class
	{
		#region Class members
		public T targetClass;
		public new SerializedObject serializedObject;
		#endregion

		#region Class overrides
		public virtual void OnEnable ()
		{
			targetClass = base.target as T;
			serializedObject = new SerializedObject (base.target);
		}
		#endregion
	}
}
#endif