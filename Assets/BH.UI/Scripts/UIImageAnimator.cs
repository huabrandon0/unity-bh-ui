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

        public void ChangeLocalPosition(Vector3 endPosition, float duration)
        {
            _image.rectTransform.DOLocalMove(endPosition, duration);
        }
    }
}
