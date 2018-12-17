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

        SerializedProperty _buttonRectTransformSettings;
        SerializedProperty _buttonImageSettings;
        SerializedProperty _buttonTextSettings;
        SerializedProperty _buttonSoundEffects;
        SerializedProperty _animatedElementSettings;

        SerializedProperty _onButtonDown;
        SerializedProperty _onButtonUp;
        SerializedProperty _onButtonEnter;
        SerializedProperty _onButtonExit;

        SerializedProperty _enterDelay;
        SerializedProperty _exitDelay;

        SerializedProperty _buttonRectTransformAnimator;
        SerializedProperty _buttonImageAnimator;
        SerializedProperty _buttonTextAnimator;

        void OnEnable()
        {
            _buttonRectTransformSettings = serializedObject.FindProperty("_buttonRectTransformSettings");
            _buttonImageSettings = serializedObject.FindProperty("_buttonImageSettings");
            _buttonTextSettings = serializedObject.FindProperty("_buttonTextSettings");
            _buttonSoundEffects = serializedObject.FindProperty("_buttonSoundEffects");
            _animatedElementSettings = serializedObject.FindProperty("_animatedElementSettings");

            _onButtonDown = serializedObject.FindProperty("_onButtonDown");
            _onButtonUp = serializedObject.FindProperty("_onButtonUp");
            _onButtonEnter = serializedObject.FindProperty("_onButtonEnter");
            _onButtonExit = serializedObject.FindProperty("_onButtonExit");

            _enterDelay = serializedObject.FindProperty("_enterDelay");
            _exitDelay = serializedObject.FindProperty("_exitDelay");

            _buttonRectTransformAnimator = serializedObject.FindProperty("_buttonRectTransformAnimator");
            _buttonImageAnimator = serializedObject.FindProperty("_buttonImageAnimator");
            _buttonTextAnimator = serializedObject.FindProperty("_buttonTextAnimator");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_buttonRectTransformSettings);
            EditorGUILayout.PropertyField(_buttonImageSettings);
            EditorGUILayout.PropertyField(_buttonTextSettings);
            EditorGUILayout.PropertyField(_buttonSoundEffects);
            EditorGUILayout.PropertyField(_animatedElementSettings);

            _foldout.animations = EditorGUILayout.Foldout(_foldout.animations, "Enter/Exit Animation");
            if (_foldout.animations)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_enterDelay);
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

            EditorGUILayout.PropertyField(_buttonRectTransformAnimator);
            EditorGUILayout.PropertyField(_buttonImageAnimator);
            EditorGUILayout.PropertyField(_buttonTextAnimator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
