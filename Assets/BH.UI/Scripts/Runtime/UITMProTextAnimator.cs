using UnityEngine;
using TMPro;
using DG.Tweening;

namespace BH.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITMProTextAnimator : UIRectTransformAnimator
    {
        TextMeshProUGUI _text;

        protected override void Awake()
        {
            base.Awake();
            if (!_text)
                _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetAlpha(float alpha)
        {
            if (!_text)
                _text = GetComponent<TextMeshProUGUI>();

            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
        }

        public void ChangeAlpha(float endAlpha, float duration)
        {
            if (!_text)
                _text = GetComponent<TextMeshProUGUI>();

            _text.DOFade(endAlpha, duration);
        }

        public void SetColor(Color color)
        {
            if (!_text)
                _text = GetComponent<TextMeshProUGUI>();

            _text.color = color;
        }

        public void ChangeColor(Color endColor, float duration)
        {
            if (!_text)
                _text = GetComponent<TextMeshProUGUI>();

            _text.DOColor(endColor, duration);
        }
    }
}