using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    [CreateAssetMenu(fileName = "New UIButtonTextSettings", menuName = "BH.UI/UIButtonTextSettings", order = 3)]
    public class UIButtonTextSettings : ScriptableObject
    {
        public Color _idleColor = Color.black;
        public Color _hoveredOverColor = Color.white;
        public Color _pressedDownColor = Color.white;
        public float _changeColorDuration = 0.1f;

        public Vector3 _idleScale = Vector3.one;
        public Vector3 _hoveredOverScale = Vector3.one * 1.1f;
        public Vector3 _pressedDownScale = Vector3.one * 0.9f;
        public float _changeScaleDuration = 0.1f;

        public Vector3 _idleAnchoredPosition3D = Vector3.zero;
        public Vector3 _hoveredOverAnchoredPosition3D = Vector3.zero;
        public Vector3 _pressedDownAnchoredPosition3D = Vector3.zero;
        public float _changeAnchoredPosition3DDuration = 0.1f;

        void OnValidate()
        {
            _changeColorDuration = Mathf.Max(_changeColorDuration, 0f);
            _changeScaleDuration = Mathf.Max(_changeScaleDuration, 0f);
            _changeAnchoredPosition3DDuration = Mathf.Max(_changeAnchoredPosition3DDuration, 0f);
        }
    }
}
