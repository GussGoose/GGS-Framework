#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CustomGenericMenu {

	#region Class implementation
	public static void Draw (System.Action<string> onOptionSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();

		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			menu.AddItem (new GUIContent (option), false, delegate {
				onOptionSelected.Invoke (option);
			});
		}

		menu.ShowAsContext ();
	}

	public static void Draw (System.Action<int> onOptionSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();
		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			int selectedOption = i;
			menu.AddItem (new GUIContent (option), false, delegate {
				onOptionSelected.Invoke (selectedOption);
			});
		}

		menu.ShowAsContext ();
	}

	public static void Draw (System.Action<string> onOptionSelected, string defaultSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();

		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			menu.AddItem (new GUIContent (option), option == defaultSelected, delegate {
				onOptionSelected.Invoke (option);
			});
		}

		menu.ShowAsContext ();
	}

	public static void Draw (System.Action<int> onOptionSelected, int defaultSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();
		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			int selectedOption = i;
			menu.AddItem (new GUIContent (option), selectedOption == defaultSelected, delegate {
				onOptionSelected.Invoke (selectedOption);
			});
		}

		menu.ShowAsContext ();
	}

	public static void Draw (Rect rect, System.Action<string> onOptionSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();

		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			menu.AddItem (new GUIContent (option), false, delegate {
				onOptionSelected.Invoke (option);
			});
		}

		menu.DropDown (rect);
	}

	public static void Draw (Rect rect, System.Action<int> onOptionSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();

		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			int selectedOption = i;
			menu.AddItem (new GUIContent (option), false, delegate {
				onOptionSelected.Invoke (selectedOption);
			});
		}

		menu.DropDown (rect);
	}

	public static void Draw (Rect rect, System.Action<string> onOptionSelected, string defaultSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();

		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			menu.AddItem (new GUIContent (option), option == defaultSelected, delegate {
				onOptionSelected.Invoke (option);
			});
		}

		menu.DropDown (rect);
	}

	public static void Draw (Rect rect, System.Action<int> onOptionSelected, int defaultSelected, params string[] options) {
		GenericMenu menu = new GenericMenu ();

		for (int i = 0; i < options.Length; i++) {
			string option = options[i];
			int selectedOption = i;
			menu.AddItem (new GUIContent (option), selectedOption == defaultSelected, delegate {
				onOptionSelected.Invoke (selectedOption);
			});
		}

		menu.DropDown (rect);
	}

	//public static void Draw (System.Action<int> onOptionSelected, params string[] options) {
	//	GenericMenu menu = new GenericMenu ();

	//	for (int i = 0; i < options.Length; i++) {
	//		string option = options[i];
	//		menu.AddItem (new GUIContent (option), false, delegate {
	//			onOptionSelected.Invoke (i);
	//		});
	//	}

	//	menu.ShowAsContext ();
	//}
	#endregion

	#region Interface implementation
	#endregion
}
#endif