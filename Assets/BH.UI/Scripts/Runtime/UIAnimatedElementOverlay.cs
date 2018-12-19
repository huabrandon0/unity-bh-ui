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

        void Awake()
        {
            _uiAnimatedElements = GetComponentsInChildren<UIAnimatedElement>().ToList();
            _enterDuration = (from x in _uiAnimatedElements select x.AnimatedElementSettings._enterDuration + x.EnterDelay).Max();
            _exitDuration = (from x in _uiAnimatedElements select x.AnimatedElementSettings._exitDuration + x.ExitDelay).Max();
        }

        public void Enter(NoArgDelegate callback = null)
        {
            StartCoroutine(AsyncEnter(callback));
        }

        public void Exit(NoArgDelegate callback = null)
        {
            StartCoroutine(AsyncExit(callback));
        }

        IEnumerator AsyncEnter(NoArgDelegate callback = null)
        {
            AnimateEnter();
            yield return new WaitForSeconds(_enterDuration);
            if (callback != null)
                callback.Invoke();
        }

        IEnumerator AsyncExit(NoArgDelegate callback = null)
        {
            AnimateExit();
            yield return new WaitForSeconds(_exitDuration);
            if (callback != null)
                callback.Invoke();
        }

        void AnimateEnter()
        {
            foreach (UIAnimatedElement uiElementAnimator in _uiAnimatedElements)
            {
                switch (uiElementAnimator)
                {
                    case UIButton button:
                        button.DisableRaycast();
                        button.Enter(() => button.EnableRaycast());
                        break;
                    case UIAnimatedElement element:
                        element.Enter();
                        break;
                }
            }
        }

        void AnimateExit()
        {
            foreach (UIAnimatedElement uiElementAnimator in _uiAnimatedElements)
            {
                switch (uiElementAnimator)
                {
                    case UIButton button:
                        button.DisableRaycast();
                        button.Exit(); // Removed callback: () => button.EnableRaycast()
                        break;
                    case UIAnimatedElement element:
                        element.Exit();
                        break;
                }
            }
        }
    }
}
