using System.Collections;
using UnityEngine;

namespace BH.UI
{
    public class UIAnimatedElement : MonoBehaviour
    {
        [SerializeField] protected Vector3 _enterFrom = Vector3.down * 100f;
        [SerializeField] protected Vector3 _enterTo = Vector3.zero;
        protected float _enterFromAlpha = 0f;
        protected float _enterToAlpha = 1f;
        [SerializeField] protected float _enterDuration = 1f;
        [SerializeField] protected float _enterDelay = 0f;
        [SerializeField] protected Vector3 _exitTo = Vector3.down * 100f;
        [SerializeField] protected float _exitDuration = 1f;
        [SerializeField] protected float _exitDelay = 0f;
        protected bool _isAnimating = false;
        
        [SerializeField] protected IUIElementAnimator _elementAnimator;

        public void Enter()
        {
            StartCoroutine(Enter(_enterDuration, _enterDelay));
        }

        IEnumerator Enter(float duration, float delay)
        {
            if (_isAnimating || _elementAnimator == null)
                yield break;

            _isAnimating = true;
            _elementAnimator.SetAlpha(_enterFromAlpha);
            _elementAnimator.SetAnchoredPosition3D(_enterFrom);
            yield return new WaitForSeconds(delay);
            _elementAnimator.ChangeAlpha(_enterToAlpha, duration);
            _elementAnimator.ChangeAnchoredPosition3D(_enterTo, duration);
            yield return new WaitForSeconds(duration);
            _isAnimating = false;
        }

        public void Exit()
        {
            StartCoroutine(Exit(_exitDuration, _exitDelay));
        }

        IEnumerator Exit(float duration, float delay)
        {
            if (_isAnimating || _elementAnimator == null)
                yield break;

            _isAnimating = true;
            yield return new WaitForSeconds(delay);
            _elementAnimator.ChangeAlpha(0f, duration);
            _elementAnimator.ChangeAnchoredPosition3D(_exitTo, duration);
            yield return new WaitForSeconds(duration);
            _isAnimating = false;
        }
    }
}
