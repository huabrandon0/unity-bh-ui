using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BH.UI
{
    [RequireComponent(typeof(Image))]
    public class UIImageAnimator : MonoBehaviour
    {
        Image _image;

        void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void ChangeAlpha(float endAlpha, float duration)
        {
            _image.DOFade(endAlpha, duration);
        }

        public void ChangeColor(Color endColor, float duration)
        {
            _image.DOColor(endColor, duration);
        }

        public void ChangeScale(Vector3 endScale, float duration)
        {
            _image.rectTransform.DOScale(endScale, duration);
        }

        public void ChangeScale(float endScale, float duration)
        {
            _image.rectTransform.DOScale(endScale, duration);
        }

        public void ChangeAnchoredPosition3D(Vector3 endPosition, float duration)
        {
            _image.rectTransform.DOAnchorPos3D(endPosition, duration);
        }

        public void SetColor(Color color)
        {
            _image = GetComponent<Image>();
            _image.color = color;
        }

        public void SetScale(Vector3 scale)
        {
            _image = GetComponent<Image>();
            _image.rectTransform.localScale = scale;
        }

        public void SetScale(float scale)
        {
            SetScale(Vector3.one * scale);
        }

        public void SetAnchoredPosition3D(Vector3 position)
        {
            _image = GetComponent<Image>();
            _image.rectTransform.anchoredPosition3D = position;
        }
    }
}
