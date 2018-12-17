using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    public class ExampleMenu : Singleton<ExampleMenu>
    {
        [SerializeField] UIAnimatedElementOverlay _mainOverlay;

        protected override void Awake()
        {
            base.Awake();
            if (!_mainOverlay)
                _mainOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }

        void Start()
        {
            _mainOverlay.Enter();
        }

        void OnValidate()
        {
            if (!_mainOverlay)
                _mainOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }
    }
}
