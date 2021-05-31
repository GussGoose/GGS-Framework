using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor.Examples
{
    public class ReorderableListExampleEditorWindow : EditorWindow
    {
        #region Members
        [SerializeField] private List<ReorderableListExampleClass> list = new List<ReorderableListExampleClass> ();

        [SerializeField] private ReorderableListState nonSerializableReorderableListState;
        private ExampleNonSerializableReorderableList nonSerializableReorderableList;

        [SerializeField] private ReorderableListState serializableReorderableListState;
        private ExampleSerializableReorderableList serializableReorderableList;
        #endregion

        #region Properties
        private Rect Rect
        {
            get { return new Rect (Vector2.zero, position.size); }
        }
        #endregion

        #region Unity Callbacks
        private void OnEnable ()
        {
            Initialize ();
        }

        private void OnGUI ()
        {
            if (nonSerializableReorderableList == null || serializableReorderableList == null)
                Initialize ();

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (Rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Object"),
                new AdvancedRect.FixedSpace (4),
                new AdvancedRect.ExpandedItem ("Serialized")
            );

            nonSerializableReorderableList.Draw (rects["Object"]);
            serializableReorderableList.Draw (rects["Serialized"]);
        }
        #endregion

        #region Implementation
        [MenuItem ("Window/GGS Framework/Examples/Reorderable List")]
        public static void Open ()
        {
            ReorderableListExampleEditorWindow window = GetWindow<ReorderableListExampleEditorWindow> ();
            window.titleContent = new GUIContent ("RL Example");
            window.Show ();
        }

        private void Initialize ()
        {
            if (nonSerializableReorderableListState == null)
                nonSerializableReorderableListState = new ReorderableListState ();

            nonSerializableReorderableList = new ExampleNonSerializableReorderableList (nonSerializableReorderableListState, list);

            if (serializableReorderableListState == null)
                serializableReorderableListState = new ReorderableListState ();

            SerializedObject serializedObject = new SerializedObject (this);
            serializableReorderableList = new ExampleSerializableReorderableList (serializableReorderableListState, serializedObject.FindProperty ("list"));
        }
        #endregion

        #region Nested Types
        public class ExampleNonSerializableReorderableList : NonSerializableReorderableList<ReorderableListExampleClass>
        {
            #region Constructors
            public ExampleNonSerializableReorderableList (ReorderableListState state, List<ReorderableListExampleClass> elements) : base (state, elements)
            {
                ShowAlternatingRowBackgrounds = true;
            }
            #endregion

            #region Implementation
            protected override bool DoesElementMatchSearch (int index, string search)
            {
                return elements[index].name.Contains (search);
            }

            protected override ReorderableListExampleClass CreateElementObject ()
            {
                return new ReorderableListExampleClass ("Tonais", 0, true);
            }

            protected override void DrawElement (Rect rect, int index)
            {
                Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                    new AdvancedRect.ProportionalItem ("Name", 50),
                    new AdvancedRect.FixedSpace (2),
                    new AdvancedRect.ExpandedItem ("Float"),
                    new AdvancedRect.FixedSpace (2),
                    new AdvancedRect.FixedItem ("Bool", 50)
                );

                EditorGUIUtility.labelWidth = 35;

                ReorderableListExampleClass element = elements[index];
                element.name = EditorGUI.TextField (rects["Name"], element.name);
                element.value = EditorGUI.Slider (rects["Float"], "Value", element.value, -10, 10);
                element.boolean = EditorGUI.Toggle (rects["Bool"], "Bool", element.boolean);

                EditorGUIUtility.labelWidth = 0;
            }
            #endregion
        }

        public class ExampleSerializableReorderableList : SerializableReorderableList
        {
            #region Constructors
            public ExampleSerializableReorderableList (ReorderableListState state, SerializedProperty elements) : base (state, elements)
            {
                ShowAlternatingRowBackgrounds = true;
            }
            #endregion

            #region Implementation
            protected override bool DoesElementMatchSearch (int index, string search)
            {
                return GetElementAtIndex (index).FindPropertyRelative ("name").stringValue.Contains (search);
            }

            protected override void AddElementAtIndex (int insertIndex)
            {
                AddElementAtIndex (insertIndex, new ReorderableListExampleClass ("Tonais", 0, true));
            }

            public override float GetElementHeight (int index)
            {
                return 30;
            }

            protected override void DrawElement (Rect rect, int index)
            {
                Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                    new AdvancedRect.ProportionalItem ("Name", 50),
                    new AdvancedRect.FixedSpace (2),
                    new AdvancedRect.ExpandedItem ("Float"),
                    new AdvancedRect.FixedSpace (2),
                    new AdvancedRect.FixedItem ("Bool", 50)
                );

                EditorGUIUtility.labelWidth = 35;

                SerializedObject.Update ();

                if (index >= ElementCount)
                    return;

                SerializedProperty property = GetElementAtIndex (index);

                EditorGUI.PropertyField (rects["Name"], property.FindPropertyRelative ("name"), GUIContent.none);
                EditorGUI.Slider (rects["Float"], property.FindPropertyRelative ("value"), -10, 10, "Value");
                EditorGUI.PropertyField (rects["Bool"], property.FindPropertyRelative ("boolean"), new GUIContent ("Bool"));

                SerializedObject.ApplyModifiedProperties ();

                EditorGUIUtility.labelWidth = 0;
            }
            #endregion
        }
        #endregion
    }

    [Serializable]
    public class ReorderableListExampleClass
    {
        #region Class Members
        public string name;
        public float value;
        public bool boolean;
        #endregion

        #region Constructors
        public ReorderableListExampleClass (string name, float value, bool boolean)
        {
            this.name = name;
            this.value = value;
            this.boolean = boolean;
        }
        #endregion
    }
}