using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static partial class ExtensionMethods {

	#region Class implementation
	public static void MoveUp<T> (this List<T> list, int index) {
		T currentElement = list[index];
		if (index - 1 >= 0) {
			T upElement = list[index - 1];
			list[index] = upElement;
			list[index - 1] = currentElement;
		}
	}

	public static void MoveDown<T> (this List<T> list, int index) {
		T currentElement = list[index];
		if (index + 1 < list.Count) {
			T upElement = list[index + 1];
			list[index] = upElement;
			list[index + 1] = currentElement;
		}
	}

	public static void InsertAbove<T> (this List<T> list, int index) {
		IList tempList = list;
		tempList.Insert (index, GetElementTypeOfList (tempList));
		list = (List<T>) tempList;

		//IList tempList = list;
		//System.Type elementType = tempList.GetType ().GetGenericArguments ().Single ();
		//object newObject = System.Activator.CreateInstance (elementType);
		//tempList.Insert (index, newObject);
		//list = (List<T>) tempList;
	}

	public static void InsertBelow<T> (this List<T> list, int index) {
		IList tempList = list;
		System.Type elementType = tempList.GetType ().GetGenericArguments ().Single ();
		object newObject = System.Activator.CreateInstance (elementType);
		tempList.Insert (index + 1, newObject);
		list = (List<T>) tempList;
	}

	public static void Duplicate<T> (this List<T> list, int index) {
		IList tempList = list;
		//System.Type elementType = tempList.GetType ().GetGenericArguments ().Single ();

		object element = list[index];
		object newObject = element.CloneObject ();

		tempList.Insert (index + 1, newObject);
		list = (List<T>) tempList;
	}
	#endregion
}