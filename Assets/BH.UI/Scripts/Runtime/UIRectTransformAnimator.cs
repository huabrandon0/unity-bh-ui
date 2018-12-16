using UnityEngine;
using DG.Tweening;

namespace BH.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIRectTransformAnimator : MonoBehaviour, IUIElementAnimator
    {
        RectTransform _rectTransform;

        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public abstract void SetAlpha(float alpha);
        public abstract void ChangeAlpha(float endAlpha, float duration);
        public abstract void ChangeAlpha(float startAlpha, float endAlpha, float duration);
        public abstract void SetColor(Color color);
        public abstract void ChangeColor(Color endColor, float duration);
        public abstract void ChangeColor(Color startColor, Color endColor, float duration);
        
        public void SetScale(Vector3 scale)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.localScale = scale;
        }

        public void SetScale(float scale)
        {
            SetScale(Vector3.one * scale);
        }

        public void ChangeScale(Vector3 endScale, float duration)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.DOScale(endScale, duration);
        }

        public void ChangeScale(float endScale, float duration)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.DOScale(endScale, duration);
        }
        
        public void SetAnchoredPosition3D(Vector3 position)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.anchoredPosition3D = position;
        }

        public void ChangeAnchoredPosition3D(Vector3 endPosition, float duration)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.DOAnchorPos3D(endPosition, duration);
        }

        public void ChangeAnchoredPosition3D(Vector3 startPosition, Vector3 endPosition, float duration)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.anchoredPosition3D = startPosition;
            ChangeAnchoredPosition3D(endPosition, duration);
        }
    }
}
