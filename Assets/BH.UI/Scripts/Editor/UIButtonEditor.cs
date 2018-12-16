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
            public static bool anchoredPosition3D = true;
            public static bool soundEffects = true;
            public static bool animations = true;
            public static bool events = true;
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
        
        SerializedProperty _playOnButtonDown;
        SerializedProperty _playOnButtonUp;
        SerializedProperty _playOnButtonEnter;
        SerializedProperty _playOnButtonExit;

        SerializedProperty _enterFrom;
        SerializedProperty _enterTo;
        SerializedProperty _enterDuration;
        SerializedProperty _enterDelay;
        SerializedProperty _exitTo;
        SerializedProperty _exitDuration;
        SerializedProperty _exitDelay;

        SerializedProperty _onButtonDown;
        SerializedProperty _onButtonUp;
        SerializedProperty _onButtonEnter;
        SerializedProperty _onButtonExit;

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

            _playOnButtonDown = serializedObject.FindProperty("_playOnButtonDown");
            _playOnButtonUp = serializedObject.FindProperty("_playOnButtonUp");
            _playOnButtonEnter = serializedObject.FindProperty("_playOnButtonEnter");
            _playOnButtonExit = serializedObject.FindProperty("_playOnButtonExit");

            _enterFrom = serializedObject.FindProperty("_enterFrom");
            _enterTo = serializedObject.FindProperty("_enterTo");
            _enterDuration = serializedObject.FindProperty("_enterDuration");
            _enterDelay = serializedObject.FindProperty("_enterDelay");
            _exitTo = serializedObject.FindProperty("_exitTo");
            _exitDuration = serializedObject.FindProperty("_exitDuration");
            _exitDelay = serializedObject.FindProperty("_exitDelay");

            _onButtonDown = serializedObject.FindProperty("_onButtonDown");
            _onButtonUp = serializedObject.FindProperty("_onButtonUp");
            _onButtonEnter = serializedObject.FindProperty("_onButtonEnter");
            _onButtonExit = serializedObject.FindProperty("_onButtonExit");
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

            _foldout.anchoredPosition3D = EditorGUILayout.Foldout(_foldout.anchoredPosition3D, "Anchored Position");
            if (_foldout.anchoredPosition3D)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_idleAnchoredPosition3D);
                EditorGUILayout.PropertyField(_hoveredOverAnchoredPosition3D);
                EditorGUILayout.PropertyField(_pressedDownAnchoredPosition3D);
                EditorGUILayout.PropertyField(_changeAnchoredPosition3DDuration);
                EditorGUI.indentLevel--;
            }

            _foldout.soundEffects = EditorGUILayout.Foldout(_foldout.soundEffects, "Sound Effects");
            if (_foldout.soundEffects)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_playOnButtonDown);
                EditorGUILayout.PropertyField(_playOnButtonUp);
                EditorGUILayout.PropertyField(_playOnButtonEnter);
                EditorGUILayout.PropertyField(_playOnButtonExit);
                EditorGUI.indentLevel--;
            }

            _foldout.animations = EditorGUILayout.Foldout(_foldout.animations, "Enter/Exit Animations");
            if (_foldout.animations)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_enterFrom);
                EditorGUILayout.PropertyField(_enterTo);
                EditorGUILayout.PropertyField(_enterDuration);
                EditorGUILayout.PropertyField(_enterDelay);
                EditorGUILayout.PropertyField(_exitTo);
                EditorGUILayout.PropertyField(_exitDuration);
                EditorGUILayout.PropertyField(_exitDelay);
                EditorGUI.indentLevel--;
            }

            _foldout.events = EditorGUILayout.Foldout(_foldout.events, "Events");
            if (_foldout.events)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_onButtonDown);
                EditorGUILayout.PropertyField(_onButtonUp);
                EditorGUILayout.PropertyField(_onButtonEnter);
                EditorGUILayout.PropertyField(_onButtonExit);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
