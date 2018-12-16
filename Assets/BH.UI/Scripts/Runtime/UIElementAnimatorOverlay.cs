using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    public class UIElementAnimatorOverlay : MonoBehaviour
    {
        List<IUIElementAnimator> _uiElementAnimators;

        void Awake()
        {
            _uiElementAnimators = new List<IUIElementAnimator>(GetComponentsInChildren<IUIElementAnimator>());
        }

        public void Enter()
        {
            foreach (IUIElementAnimator uiElementAnimator in _uiElementAnimators)
                uiElementAnimator.Enter();
        }

        public void Exit()
        {
            foreach (IUIElementAnimator uiElementAnimator in _uiElementAnimators)
                uiElementAnimator.Exit();
        }
    }
}
