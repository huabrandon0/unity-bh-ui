using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BH.UI
{
    public class UIAnimatedElementOverlay : MonoBehaviour
    {
        List<UIAnimatedElement> _uiAnimatedElements;

        float _enterDuration = 0f;
        float _exitDuration = 0f;

        public delegate void NoArgDelegate();

        void Awake()
        {
            _uiAnimatedElements = new List<UIAnimatedElement>(GetComponentsInChildren<UIAnimatedElement>());
            _enterDuration = (from x in _uiAnimatedElements select x.AnimatedElementSettings._enterDuration + x.EnterDelay).Max();
            _exitDuration = (from x in _uiAnimatedElements select x.AnimatedElementSettings._exitDuration + x.ExitDelay).Max();
        }

        public void Enter()
        {
            foreach (UIAnimatedElement uiElementAnimator in _uiAnimatedElements)
                uiElementAnimator.Enter();
        }
        
        public IEnumerator Enter(NoArgDelegate callback)
        {
            Enter();
            yield return new WaitForSeconds(_enterDuration);
            callback.Invoke();
        }
        
        public void Exit()
        {
            foreach (UIAnimatedElement uiElementAnimator in _uiAnimatedElements)
                uiElementAnimator.Exit();
        }

        public IEnumerator Exit(NoArgDelegate callback)
        {
            Exit();
            yield return new WaitForSeconds(_exitDuration);
            callback.Invoke();
        }
    }
}
