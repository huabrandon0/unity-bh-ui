using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    [CreateAssetMenu(fileName = "New UIAnimatedElementSettings", menuName = "BH.UI/UIAnimatedElementSettings", order = 5)]
    public class UIAnimatedElementSettings : ScriptableObject
    {
        public Vector3 _enterFrom = Vector3.down * 100f;
        public Vector3 _enterTo = Vector3.zero;
        public float _enterFromAlpha = 0f;
        public float _enterToAlpha = 1f;
        public float _enterDuration = 1f;
        public Vector3 _exitTo = Vector3.down * 100f;
        public float _exitDuration = 1f;

        void OnValidate()
        {
            _enterFromAlpha = Mathf.Clamp(_enterFromAlpha, 0f, 1f);
            _enterToAlpha = Mathf.Clamp(_enterToAlpha, 0f, 1f);
            _enterDuration = Mathf.Max(_enterDuration, 0f);
            _exitDuration = Mathf.Max(_exitDuration, 0f);
        }
    }
}
