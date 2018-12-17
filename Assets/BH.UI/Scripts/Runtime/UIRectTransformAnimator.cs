﻿using UnityEngine;
using DG.Tweening;

namespace BH.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIRectTransformAnimator : MonoBehaviour
    {
        RectTransform _rectTransform;

        protected virtual void Awake()
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();
        }
        
        public void SetScale(Vector3 scale)
        {
            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.localScale = scale;
        }

        public void ChangeScale(Vector3 endScale, float duration)
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
    }
}
