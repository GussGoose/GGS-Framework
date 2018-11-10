//using UnityEngine;

//public static partial class ExtensionMethods {

//	#region Class implementation
//	public static IDamagable GetDamagable (this Component thisObject) {
//		return thisObject.GetComponent<IDamagable> ();
//	}

//	public static IDamagable GetDamagableInAll (this Component thisObject) {
//		IDamagable damagable = thisObject.GetComponentInChildren<IDamagable> ();

//		if (damagable == null)
//			damagable = thisObject.GetComponentInParent<IDamagable> ();

//		return damagable;
//	}
//	#endregion
//}