using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    [CreateAssetMenu(fileName = "New UIButtonImageSettings", menuName = "BH.UI/UIButtonImageSettings", order = 2)]
    public class UIButtonImageSettings : ScriptableObject
    {
        public Color _idleColor = Color.white;
        public Color _hoveredOverColor = Color.gray;
        public Color _pressedDownColor = Color.black;
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
