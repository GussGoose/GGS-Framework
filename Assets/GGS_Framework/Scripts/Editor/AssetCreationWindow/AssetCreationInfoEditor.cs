using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public class AssetCreationInfoEditor
    {
        #region Members
        protected readonly SerializedObject serializedObject;
        #endregion

        #region Constructors
        public AssetCreationInfoEditor (SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
        }
        #endregion

        #region Implementation
        public virtual float GetLabelWidth ()
        {
            float maxFoundWidth = 0;

            SerializedProperty property = serializedObject.GetIterator ();
            if (property.NextVisible (true))
            {
                while (property.NextVisible (false))
                {
                    float currentWidth = EditorStyles.label.CalcSize (new GUIContent (property.displayName)).x;

                    if (currentWidth > maxFoundWidth)
                        maxFoundWidth = currentWidth;
                }
            }

            return maxFoundWidth + 5;
        }

        public virtual void OnGUI ()
        {
            SerializedProperty property = serializedObject.GetIterator ();
            if (property.NextVisible (true))
            {
                while (property.NextVisible (false))
                {
                    EditorGUI.BeginChangeCheck ();
                    EditorGUILayout.PropertyField (property);
                    if (EditorGUI.EndChangeCheck ())
                    {
                        EditorUtility.SetDirty (serializedObject.targetObject);
                        property.serializedObject.ApplyModifiedProperties ();
                    }
                }
            }
        }
        #endregion
    }
}