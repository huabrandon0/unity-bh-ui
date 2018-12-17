using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace BH.UI
{
    [AddComponentMenu("UI/BH.UI - UITMProText")]
    public class UITMProText : UIAnimatedElement
    {
        [MenuItem("GameObject/UI/BH.UI - UITMProText")]
        static void CreateUIImage(MenuCommand menuCommand)
        {
            // Check if there is a Canvas in the scene.
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                // Create new Canvas since none exists in the scene.
                GameObject canvasObject = new GameObject("Canvas");
                canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                // Add a Canvas Scaler Component.
                canvas.gameObject.AddComponent<CanvasScaler>();

                // Add a Graphic Raycaster Component.
                canvas.gameObject.AddComponent<GraphicRaycaster>();

                Undo.RegisterCreatedObjectUndo(canvasObject, "Create " + canvasObject.name);
            }

            GameObject text = new GameObject("BH.UI - UITMProText");
            text.AddComponent<RectTransform>();
            Selection.activeObject = text;
            UITMProText textUIText = text.AddComponent(typeof(UITMProText)) as UITMProText;

            Undo.RegisterCreatedObjectUndo(text, "Create " + text.name);

            GameObject textText = new GameObject("AnimatedText");
            textText.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(textText, text);
            textText.AddComponent(typeof(TextMeshProUGUI));
            textUIText._textAnimator = textText.AddComponent(typeof(UITMProTextAnimator)) as UITMProTextAnimator;
            textUIText._rectTransformAnimator = textUIText._textAnimator;

            Undo.RegisterCreatedObjectUndo(textText, "Create " + textText.name);

            // Check if object is being created with left click or right click.
            GameObject contextObject = menuCommand.context as GameObject;
            if (contextObject == null)
                GameObjectUtility.SetParentAndAlign(text, canvas.gameObject);
            else
                GameObjectUtility.SetParentAndAlign(text, contextObject);

            // Check if an event system already exists in the scene.
            if (!FindObjectOfType<EventSystem>())
            {
                GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem));
                eventObject.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventObject, "Create " + eventObject.name);
            }
        }

        [SerializeField] protected UIRectTransformAnimator _rectTransformAnimator;
        [SerializeField] protected UITMProTextAnimator _textAnimator;

        void Awake()
        {
            if (!_rectTransformAnimator)
                _rectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();

            if (!_textAnimator)
                _textAnimator = GetComponentInChildren<UITMProTextAnimator>();
        }

        public override void Enter()
        {
            StartCoroutine(Enter(_animatedElementSettings._enterDuration, _enterDelay));
        }

        IEnumerator Enter(float duration, float delay)
        {
            if (_isAnimating || _rectTransformAnimator == null || _textAnimator == null)
                yield break;

            _isAnimating = true;
            _rectTransformAnimator.SetAnchoredPosition3D(_animatedElementSettings._enterFrom);
            _textAnimator.SetAlpha(_animatedElementSettings._enterFromAlpha);
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._enterTo, duration);
            _textAnimator.ChangeAlpha(_animatedElementSettings._enterToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _isAnimating = false;
        }

        public override void Exit()
        {
            StartCoroutine(Exit(_animatedElementSettings._exitDuration, _exitDelay));
        }

        IEnumerator Exit(float duration, float delay)
        {
            if (_isAnimating || _rectTransformAnimator == null || _textAnimator == null)
                yield break;

            _isAnimating = true;
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._exitTo, duration);
            _textAnimator.ChangeAlpha(_animatedElementSettings._exitToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _isAnimating = false;
        }

        void OnValidate()
        {
            if (!_rectTransformAnimator)
                _rectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();

            if (!_textAnimator)
                _textAnimator = GetComponentInChildren<UITMProTextAnimator>();

            _enterDelay = Mathf.Max(_enterDelay, 0f);
            _exitDelay = Mathf.Max(_exitDelay, 0f);
        }
    }
}
