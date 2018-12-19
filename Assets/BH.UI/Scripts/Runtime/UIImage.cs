using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

namespace BH.UI
{
    [AddComponentMenu("UI/BH.UI - UIImage")]
    public class UIImage : UIAnimatedElement
    {
        public UIRectTransformAnimator _rectTransformAnimator;
        public UIImageAnimator _imageAnimator;

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
            if (_rectTransformAnimator == null || _imageAnimator == null)
                yield break;
            
            _rectTransformAnimator.SetAnchoredPosition3D(_animatedElementSettings._enterFrom);
            _imageAnimator.SetAlpha(_animatedElementSettings._enterFromAlpha);
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._enterTo, duration);
            _imageAnimator.ChangeAlpha(_animatedElementSettings._enterToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;

            if (callback != null)
                callback.Invoke();
        }

        public override void Exit(NoArgDelegate callback = null)
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Exit(_animatedElementSettings._exitDuration, _exitDelay));
        }

        IEnumerator Exit(float duration, float delay, NoArgDelegate callback = null)
        {
            if (_rectTransformAnimator == null || _imageAnimator == null)
                yield break;
            
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._exitTo, duration);
            _imageAnimator.ChangeAlpha(_animatedElementSettings._exitToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;

            if (callback != null)
                callback.Invoke();
        }

        void OnValidate()
        {
            if (!_rectTransformAnimator)
                _rectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();
            
            if (!_imageAnimator)
                _imageAnimator = GetComponentInChildren<UIImageAnimator>();

            _enterDelay = Mathf.Max(_enterDelay, 0f);
            _exitDelay = Mathf.Max(_exitDelay, 0f);
        }
    }
}
