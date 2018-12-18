using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BH.UI
{
    [RequireComponent(typeof(Image))]
    public class UIImageAnimator : UIRectTransformAnimator
    {
        Image _image;
        
        Tweener _colorTweener;

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

            if (_colorTweener != null)
                _colorTweener.Kill();

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
        }

        public void ChangeAlpha(float endAlpha, float duration)
        {
            if (!_image)
                _image = GetComponent<Image>();

            if (_colorTweener != null)
                _colorTweener.Kill();

            _colorTweener = _image.DOFade(endAlpha, duration);
        }

        public void SetColor(Color color)
        {
            if (!_image)
                _image = GetComponent<Image>();

            if (_colorTweener != null)
                _colorTweener.Kill();

            _image.color = color;
        }

        public void ChangeColor(Color endColor, float duration)
        {
            if (!_image)
                _image = GetComponent<Image>();

            if (_colorTweener != null)
                _colorTweener.Kill();

            _colorTweener = _image.DOColor(endColor, duration);
        }
    }
}
