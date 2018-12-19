using UnityEngine;
using System.Collections;

namespace BH.UI
{
    [AddComponentMenu("UI/BH.UI - UITMProText")]
    public class UITMProText : UIAnimatedElement
    {
        public UIRectTransformAnimator _rectTransformAnimator;
        public UITMProTextAnimator _textAnimator;

        Coroutine _animation;

        void Awake()
        {
            OnValidate();
        }

        public override void Enter(NoArgDelegate callback = null)
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Enter(_animatedElementSettings._enterDuration, _enterDelay, callback));
        }

        IEnumerator Enter(float duration, float delay, NoArgDelegate callback = null)
        {
            if (_rectTransformAnimator == null || _textAnimator == null)
                yield break;
            
            _rectTransformAnimator.SetAnchoredPosition3D(_animatedElementSettings._enterFrom);
            _textAnimator.SetAlpha(_animatedElementSettings._enterFromAlpha);
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._enterTo, duration);
            _textAnimator.ChangeAlpha(_animatedElementSettings._enterToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;

            if (callback != null)
                callback.Invoke();
        }

        public override void Exit(NoArgDelegate callback = null)
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Exit(_animatedElementSettings._exitDuration, _exitDelay, callback));
        }

        IEnumerator Exit(float duration, float delay, NoArgDelegate callback = null)
        {
            if (_rectTransformAnimator == null || _textAnimator == null)
                yield break;
            
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._exitTo, duration);
            _textAnimator.ChangeAlpha(_animatedElementSettings._exitToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;
            
            if (callback != null)
                callback.Invoke();
        }

        void OnValidate()
        {
            if (!_rectTransformAnimator)
                _rectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();

            if (!_textAnimator)
                _textAnimator = GetComponentInChildren<UITMProTextAnimator>();

            _enterDelay = Mathf.Max(_enterDelay, 0f);
            _exitDelay = Mathf.Max(_exitDelay, 0f);
        }
    }
}
