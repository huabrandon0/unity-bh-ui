using UnityEngine;

namespace BH.UI
{
    public interface IUIElementAnimator
    {
        void SetAlpha(float alpha);
        void ChangeAlpha(float endAlpha, float duration);
        void ChangeAlpha(float startAlpha, float endAlpha, float duration);
        void SetColor(Color color);
        void ChangeColor(Color endColor, float duration);
        void ChangeColor(Color startColor, Color endColor, float duration);
        void SetScale(Vector3 scale);
        void SetScale(float scale);
        void ChangeScale(Vector3 endScale, float duration);
        void ChangeScale(float endScale, float duration);
        void SetAnchoredPosition3D(Vector3 position);
        void ChangeAnchoredPosition3D(Vector3 endPosition, float duration);
        void ChangeAnchoredPosition3D(Vector3 startPosition, Vector3 endPosition, float duration);
    }
}
