using UnityEngine;

namespace GGS_Framework
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		#region Class Members
		private static T instance;
		#endregion

		#region Class Accesors
		public static T Instance
		{
			get
			{
				if (instance == null)
					instance = FindObjectOfType<T> ();

				return instance;
			}
		}
		#endregion

		#region Class Overrides
		public virtual void Awake ()
		{
			if (instance == null)
				instance = FindObjectOfType<T> ();
		}
		#endregion
	}
}