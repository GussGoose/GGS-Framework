using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGS_Framework.Editor
{
    public static class GGS_FrameworkEditorUtility
    {
        public static void ChangeScriptableObjectScriptType (SerializedObject serializedObject, Type newScriptType)
        {
            ScriptableObject tempSO = ScriptableObject.CreateInstance (newScriptType);

            serializedObject.Update ();
            SerializedProperty scriptProperty = serializedObject.FindProperty ("m_Script");
            scriptProperty.objectReferenceValue = MonoScript.FromScriptableObject (tempSO);
            scriptProperty.serializedObject.ApplyModifiedPropertiesWithoutUndo ();

            Object.DestroyImmediate (tempSO);
        }

        public static void ChangeMonoBehaviourScriptType (SerializedObject serializedObject, MonoBehaviour newScriptType)
        {
            serializedObject.Update ();
            SerializedProperty scriptProperty = serializedObject.FindProperty ("m_Script");
            scriptProperty.objectReferenceValue = MonoScript.FromMonoBehaviour (newScriptType);
            scriptProperty.serializedObject.ApplyModifiedPropertiesWithoutUndo ();
        }

        public static List<T> FindAssetsByType<T> () where T : Object
        {
            List<T> assets = new List<T> ();
            string[] guids = AssetDatabase.FindAssets ($"t:{typeof(T)}");
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath (guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T> (assetPath);

                if (asset != null)
                    assets.Add (asset);
            }

            return assets;
        }
    }
}