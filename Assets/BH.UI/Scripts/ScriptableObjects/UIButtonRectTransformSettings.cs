using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    [CreateAssetMenu(fileName = "New UIButtonRectTransformSettings", menuName = "BH.UI/UIButtonRectTransformSettings", order = 1)]
    public class UIButtonRectTransformSettings : ScriptableObject
    {
        public Vector3 _idleScale = Vector3.one;
        public Vector3 _hoveredOverScale = Vector3.one;
        public Vector3 _pressedDownScale = Vector3.one;
        public float _changeScaleDuration = 0.1f;

        public Vector3 _idleAnchoredPosition3D = Vector3.zero;
        public Vector3 _hoveredOverAnchoredPosition3D = Vector3.zero;
        public Vector3 _pressedDownAnchoredPosition3D = Vector3.zero;
        public float _changeAnchoredPosition3DDuration = 0.1f;

        void OnValidate()
        {
            _changeScaleDuration = Mathf.Max(_changeScaleDuration, 0f);
            _changeAnchoredPosition3DDuration = Mathf.Max(_changeAnchoredPosition3DDuration, 0f);
        }
    }
}
