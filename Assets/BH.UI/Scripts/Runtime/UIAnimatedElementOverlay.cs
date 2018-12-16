using System.Collections.Generic;
using UnityEngine;

namespace BH.UI
{
    public class UIAnimatedElementOverlay : MonoBehaviour
    {
        List<UIAnimatedElement> _uiElementAnimators;

        void Awake()
        {
            _uiElementAnimators = new List<UIAnimatedElement>(GetComponentsInChildren<UIAnimatedElement>());
        }

        public void Enter()
        {
            foreach (UIAnimatedElement uiElementAnimator in _uiElementAnimators)
                uiElementAnimator.Enter();
        }

        public void Exit()
        {
            foreach (UIAnimatedElement uiElementAnimator in _uiElementAnimators)
                uiElementAnimator.Exit();
        }
    }
}
