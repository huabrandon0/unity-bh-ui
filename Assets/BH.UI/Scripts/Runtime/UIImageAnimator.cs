using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BH.UI
{
    [RequireComponent(typeof(Image))]
    public class UIImageAnimator : UIRectTransformAnimator
    {
        Image _image;

        protected override void Awake()
        {
            base.Awake();
            if (!_image)
                _image = GetComponent<Image>();
        }
        
        public void SetAlpha(float alpha)
        {
            if (!_image)
                _image = GetComponent<Image>();

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
        }

        public void ChangeAlpha(float endAlpha, float duration)
        {
            if (!_image)
                _image = GetComponent<Image>();

            _image.DOFade(endAlpha, duration);
        }

        public void SetColor(Color color)
        {
            if (!_image)
                _image = GetComponent<Image>();

            _image.color = color;
        }

        public void ChangeColor(Color endColor, float duration)
        {
            if (!_image)
                _image = GetComponent<Image>();

            _image.DOColor(endColor, duration);
        }
    }
}
