using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUI
    {
        #region Implementation
        public static void PercentageSlider (Rect rect, GUIContent label, ref float[] percentages, string[] labels, PercentageSliderState state)
        {
            if (percentages.Length < 2)
                return;

            if (percentages.Sum () < 1)
            {
                percentages[percentages.Length - 1] += 1 - percentages.Sum ();
                GUI.changed = true;
            }

            // Recompute the hole array distributing evently if there's any percentages set to zero
            bool invalidPercentagesFound = false;
            for (int i = 0; i < percentages.Length; i++)
            {
                if (percentages[i] <= 0)
                {
                    invalidPercentagesFound = true;
                    break;
                }
            }

            if (invalidPercentagesFound)
            {
                float individualPercentage = (float) System.Math.Round (1f / (percentages.Length), 2);
                float residualPercentage = (1 - (individualPercentage * percentages.Length));

                for (int i = 0; i < percentages.Length; i++)
                {
                    percentages[i] = individualPercentage;

                    if (i == percentages.Length - 1)
                        percentages[i] += residualPercentage;
                }
            }

            float[] absolutePercentages = new float[percentages.Length];

            float currentAbsolute = 0;
            for (int i = 0; i < absolutePercentages.Length; i++)
            {
                currentAbsolute += percentages[i];
                absolutePercentages[i] = currentAbsolute;
            }

            for (int i = 0; i < absolutePercentages.Length; i++)
            {
                if (absolutePercentages[i] > 1f)
                {
                    absolutePercentages[i] = 1;
                    absolutePercentages[i - 1] -= 0.01f;

                    for (int j = i - 2; j >= 0; j--)
                    {
                        if (absolutePercentages[j] > absolutePercentages[j + 1] - 0.01f)
                            absolutePercentages[j] -= 0.01f;
                    }

                    // Report that the data in the GUI has changed
                    GUI.changed = true;
                }
            }

            rect.height = EditorGUIUtility.singleLineHeight;
            rect = EditorGUI.PrefixLabel (new Rect (rect), GUIUtility.GetControlID (FocusType.Passive), label);

            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            GUIStyle sliderStyle = GUI.skin.horizontalSlider;
            GUIStyle handleStyle = GUI.skin.horizontalSliderThumb;
            float handlerSize = handleStyle.fixedHeight;

            int controlID = GUIUtility.GetControlID (FocusType.Passive);

            switch (Event.current.GetTypeForControl (controlID))
            {
                case EventType.Repaint:
                {
                    sliderStyle.Draw (rect, GUIContent.none, controlID);

                    Vector2[] handlersPositions = new Vector2[absolutePercentages.Length - 1];

                    // Draw first and last handler
                    Vector2 firstHandlerPosition = new Vector2 (rect.x - handlerSize / 2, rect.center.y - handlerSize / 2);
                    handleStyle.Draw (new Rect (firstHandlerPosition, Vector2.one * handlerSize), GUIContent.none, controlID);

                    Vector2 lastHandlerPosition = new Vector2 (rect.x + rect.width - handlerSize / 2, rect.center.y - handlerSize / 2);
                    handleStyle.Draw (new Rect (lastHandlerPosition, Vector2.one * handlerSize), GUIContent.none, controlID);

                    // Draw handler style
                    for (int i = 0; i < absolutePercentages.Length - 1; i++)
                    {
                        int percentagePosition = (int) Mathf.Lerp (0, rect.width, absolutePercentages[i]);
                        Vector2 handlerPosition = new Vector2 (rect.x + percentagePosition - handlerSize / 2, rect.center.y - handlerSize / 2);
                        Rect handlerRect = new Rect (handlerPosition, Vector2.one * handlerSize);

                        handleStyle.Draw (handlerRect, GUIContent.none, controlID);
                        handlersPositions[i] = handlerRect.center;
                    }

                    // Draw name & percentage labels
                    for (int i = 0; i < absolutePercentages.Length; i++)
                    {
                        float minLabelX = (i == 0) ? rect.x : handlersPositions[i - 1].x;
                        float maxLabelX = (i == absolutePercentages.Length - 1) ? rect.xMax : handlersPositions[i].x;

                        bool canDrawLabels = labels != null && labels.Length >= i;
                        float labelWidth = 60;
                        Vector2 labelPosition = new Vector2 (minLabelX + (maxLabelX - minLabelX) / 2 - labelWidth / 2, rect.y + rect.height);
                        Vector2 labelSize = new Vector2 (labelWidth, 13);

                        GUIStyle labelStyle = new GUIStyle (EditorStyles.label);
                        labelStyle.alignment = TextAnchor.MiddleCenter;
                        labelStyle.fontSize = 10;
                        labelStyle.clipping = TextClipping.Overflow;

                        EditorGUI.LabelField (new Rect (labelPosition, labelSize), $"{(canDrawLabels ? labels[i] + "\n" : string.Empty)}{percentages[i] * 100f}%", labelStyle);
                    }

                    break;
                }
                case EventType.MouseDown:
                {
                    if (rect.Contains (Event.current.mousePosition) && Event.current.button == 0)
                    {
                        float nearestHandlerDistance = float.MaxValue;
                        float mouseXPosition = Event.current.mousePosition.x;
                        int nearestPercentageIndex = 0;

                        // Compute nearest handler to mouse
                        for (int i = 0; i < absolutePercentages.Length - 1; i++)
                        {
                            int percentagePosition = (int) Mathf.Lerp (0, rect.width, absolutePercentages[i]);
                            float distanceToMouse = Mathf.Abs (rect.x + percentagePosition - mouseXPosition);

                            if (distanceToMouse < nearestHandlerDistance)
                            {
                                nearestHandlerDistance = distanceToMouse;
                                nearestPercentageIndex = i;
                            }
                        }

                        state.PicketValueIndex = nearestPercentageIndex;
                        GUIUtility.hotControl = controlID;
                    }

                    break;
                }

                case EventType.MouseUp:
                {
                    // If we were the hotControl, we aren't any more.
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                        state.PicketValueIndex = -1;
                    }

                    break;
                }
            }

            if (Event.current.isMouse && GUIUtility.hotControl == controlID)
            {
                // Get left and right limits of handler
                float leftLimit = (state.PicketValueIndex == 0) ? 0.01f : absolutePercentages[state.PicketValueIndex - 1] + 0.01f;
                float rightLimit = (state.PicketValueIndex == absolutePercentages.Length - 1) ? 1 : absolutePercentages[state.PicketValueIndex + 1] - 0.01f;

                // Get mouse x position relative to left edge of the control
                float relativeX = Event.current.mousePosition.x - rect.x;
                float percentage = (float) Math.Round (Mathf.Clamp01 (relativeX / rect.width), 2);
                absolutePercentages[state.PicketValueIndex] = Mathf.Clamp (percentage, leftLimit, rightLimit);

                // Report that the data in the GUI has changed
                GUI.changed = true;

                // Mark event as 'used' so other controls don't respond to it, and to
                // trigger an automatic repaint.
                Event.current.Use ();
            }

            if (GUI.changed)
            {
                for (int i = 0; i < percentages.Length; i++)
                {
                    float absoluteValue = (i != 0) ? absolutePercentages[i] - absolutePercentages[i - 1] : absolutePercentages[i];
                    percentages[i] = (float) Math.Round (absoluteValue, 2);
                }

                return;
            }

            EditorGUI.indentLevel = indentLevel;
        }

        public static void PercentageSlider (Rect rect, string label, ref float[] percentages, string[] labels, PercentageSliderState state)
        {
            PercentageSlider (rect, new GUIContent (label), ref percentages, labels, state);
        }

        public static void PercentageSlider (Rect rect, GUIContent label, SerializedProperty[] percentagesProperties, string[] labels, PercentageSliderState state)
        {
            float[] percentages = new float[percentagesProperties.Length];

            for (int i = 0; i < percentagesProperties.Length; i++)
                percentages[i] = percentagesProperties[i].floatValue;

            EditorGUI.BeginChangeCheck ();

            PercentageSlider (rect, label, ref percentages, labels, state);

            if (EditorGUI.EndChangeCheck ())
            {
                for (int i = 0; i < percentagesProperties.Length; i++)
                    percentagesProperties[i].floatValue = percentages[i];
            }
        }

        public static void PercentageSlider (Rect rect, string label, SerializedProperty[] percentagesProperties, string[] labels, PercentageSliderState state)
        {
            PercentageSlider (rect, new GUIContent (label), percentagesProperties, labels, state);
        }
        #endregion
    }
}