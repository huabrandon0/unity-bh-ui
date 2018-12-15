using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BH.UI
{
    public class UIElementAnimatorOverlay : MonoBehaviour
    {
        List<IUIElementAnimator> _elementAnimators;

        void Awake()
        {
            _elementAnimators = GetComponentsInChildren<IUIElementAnimator>().ToList();
        }

        public void Enter()
        {
            foreach (IUIElementAnimator elementAnimator in _elementAnimators)
            {
                elementAnimator.Enter();
            }
        }

        public void Exit()
        {
            foreach (IUIElementAnimator elementAnimator in _elementAnimators)
            {
                elementAnimator.Exit();
            }
        }
    }
}
