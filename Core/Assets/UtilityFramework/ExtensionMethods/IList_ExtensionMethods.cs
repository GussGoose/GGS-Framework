namespace UtilityFramework
{
	using System;
	using System.Collections;
	using UnityEngine;

	public static partial class ExtensionMethods
	{
		#region Class implementation
		public static void InsertAbove (this IList list, int index)
		{
			list.Insert (index, GetElementTypeOfList (list));
		}

		public static void InsertBelow (this IList list, int index)
		{
			list.Insert (index + 1, GetElementTypeOfList (list));
		}

		public static void Duplicate (this IList list, int index)
		{
			object element = list[index];
			object newObject = element.CloneObject ();

			list.Insert (index + 1, newObject);
		}

		public static object GetElementTypeOfList (IList list)
		{
			//System.Type elementType = list.GetType ().GetGenericArguments ().Single ();

			//if (elementType == typeof (string))
			//	return string.Empty;
			//else if (elementType != null && elementType.GetConstructor (System.Type.EmptyTypes) == null)
			//	return null;
			//else if (list.GetType ().GetGenericArguments ()[0] != null)
			//	return Activator.CreateInstance (list.GetType ().GetGenericArguments ()[0]);
			//else if (elementType != null)
			//	return Activator.CreateInstance (elementType);
			//else
			//	return null;


			System.Type elementType = list.GetType ().GetElementType ();
			if (elementType == typeof (string))
				return (object) string.Empty;
			else if (elementType != null && elementType.GetConstructor (System.Type.EmptyTypes) == null)
				Debug.LogError ((object) ("Cannot add element. Type " + elementType.ToString () + " has no default constructor. Implement a default constructor or implement your own add behaviour."));
			else if (list.GetType ().GetGenericArguments ()[0] != null)
				return Activator.CreateInstance (list.GetType ().GetGenericArguments ()[0]);
			else if (elementType != null)
				return Activator.CreateInstance (elementType);
			else
				Debug.LogError ((object) "Cannot add element of type Null.");

			return null;
		}
		#endregion
	} 
}