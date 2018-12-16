using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BH.UI
{
    [CustomEditor(typeof(UIButton))]
    [CanEditMultipleObjects]
    public class UIButtonEditor : Editor
    {
        struct _foldout
        {
            // Track Inspector foldout panel states, globally.
            public static bool color = true;
            public static bool scale = true;
            public static bool AnchoredPosition3D = true;
        }

        SerializedProperty _idleColor;
        SerializedProperty _hoveredOverColor;
        SerializedProperty _pressedDownColor;
        SerializedProperty _changeColorDuration;

        SerializedProperty _idleScale;
        SerializedProperty _hoveredOverScale;
        SerializedProperty _pressedDownScale;
        SerializedProperty _changeScaleDuration;

        SerializedProperty _idleAnchoredPosition3D;
        SerializedProperty _hoveredOverAnchoredPosition3D;
        SerializedProperty _pressedDownAnchoredPosition3D;
        SerializedProperty _changeAnchoredPosition3DDuration;

        void OnEnable()
        {
            _idleColor = serializedObject.FindProperty("_idleColor");
            _hoveredOverColor = serializedObject.FindProperty("_hoveredOverColor");
            _pressedDownColor = serializedObject.FindProperty("_pressedDownColor");
            _changeColorDuration = serializedObject.FindProperty("_changeColorDuration");

            _idleScale = serializedObject.FindProperty("_idleScale");
            _hoveredOverScale = serializedObject.FindProperty("_hoveredOverScale");
            _pressedDownScale = serializedObject.FindProperty("_pressedDownScale");
            _changeScaleDuration = serializedObject.FindProperty("_changeScaleDuration");

            _idleAnchoredPosition3D = serializedObject.FindProperty("_idleAnchoredPosition3D");
            _hoveredOverAnchoredPosition3D = serializedObject.FindProperty("_hoveredOverAnchoredPosition3D");
            _pressedDownAnchoredPosition3D = serializedObject.FindProperty("_pressedDownAnchoredPosition3D");
            _changeAnchoredPosition3DDuration = serializedObject.FindProperty("_changeAnchoredPosition3DDuration");
        }

        public override void OnInspectorGUI()
        {
            _foldout.color = EditorGUILayout.Foldout(_foldout.color, "Color");
            if (_foldout.color)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_idleColor);
                EditorGUILayout.PropertyField(_hoveredOverColor);
                EditorGUILayout.PropertyField(_pressedDownColor);
                EditorGUILayout.PropertyField(_changeColorDuration);
                EditorGUI.indentLevel--;
            }

            _foldout.scale = EditorGUILayout.Foldout(_foldout.scale, "Scale");
            if (_foldout.scale)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_idleScale);
                EditorGUILayout.PropertyField(_hoveredOverScale);
                EditorGUILayout.PropertyField(_pressedDownScale);
                EditorGUILayout.PropertyField(_changeScaleDuration);
                EditorGUI.indentLevel--;
            }

            _foldout.AnchoredPosition3D = EditorGUILayout.Foldout(_foldout.AnchoredPosition3D, "Local Position");
            if (_foldout.AnchoredPosition3D)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_idleAnchoredPosition3D);
                EditorGUILayout.PropertyField(_hoveredOverAnchoredPosition3D);
                EditorGUILayout.PropertyField(_pressedDownAnchoredPosition3D);
                EditorGUILayout.PropertyField(_changeAnchoredPosition3DDuration);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
