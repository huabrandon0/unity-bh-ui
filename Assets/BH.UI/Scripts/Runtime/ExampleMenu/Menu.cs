using UnityEngine;

namespace BH.UI
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        Canvas _canvas;
        [SerializeField] protected UIAnimatedElementOverlay _mainOverlay;
        
        public delegate void NoArgDelegate();

        void Awake()
        {
            if (!_canvas)
                _canvas = GetComponent<Canvas>();

            if (!_mainOverlay)
                _mainOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }

        public void Open()
        {
            Open(() => { });
        }

        public void Open(NoArgDelegate callback)
        {
            _canvas.enabled = true;
            StartCoroutine(_mainOverlay.Enter(() => { callback.Invoke(); }));
        }

        public void Close()
        {
            Close(() => { });
        }

        public void Close(NoArgDelegate callback)
        {
            StartCoroutine(_mainOverlay.Exit(() => { _canvas.enabled = false; callback.Invoke(); }));
        }

        void OnValidate()
        {
            if (!_canvas)
                _canvas = GetComponent<Canvas>();

            if (!_mainOverlay)
                _mainOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }
    }
}
