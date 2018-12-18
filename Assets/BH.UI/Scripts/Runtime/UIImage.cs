using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

namespace BH.UI
{
    [AddComponentMenu("UI/BH.UI - UIImage")]
    public class UIImage : UIAnimatedElement
    {
        [MenuItem("GameObject/UI/BH.UI - UIImage")]
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

            GameObject image = new GameObject("BH.UI - UIImage");
            image.AddComponent<RectTransform>();
            Selection.activeObject = image;
            UIImage imageUIImage = image.AddComponent(typeof(UIImage)) as UIImage;

            Undo.RegisterCreatedObjectUndo(image, "Create " + image.name);

            GameObject imageImage = new GameObject("AnimatedImage");
            imageImage.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(imageImage, image);
            imageImage.AddComponent(typeof(Image));
            imageUIImage._imageAnimator = imageImage.AddComponent(typeof(UIImageAnimator)) as UIImageAnimator;
            imageUIImage._rectTransformAnimator = imageUIImage._imageAnimator;

            Undo.RegisterCreatedObjectUndo(imageImage, "Create " + imageImage.name);

            // Check if object is being created with left click or right click.
            GameObject contextObject = menuCommand.context as GameObject;
            if (contextObject == null)
                GameObjectUtility.SetParentAndAlign(image, canvas.gameObject);
            else
                GameObjectUtility.SetParentAndAlign(image, contextObject);

            // Check if an event system already exists in the scene.
            if (!FindObjectOfType<EventSystem>())
            {
                GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem));
                eventObject.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventObject, "Create " + eventObject.name);
            }
        }

        [SerializeField] protected UIRectTransformAnimator _rectTransformAnimator;
        [SerializeField] protected UIImageAnimator _imageAnimator;

        Coroutine _animation;

        void Awake()
        {
            if (!_rectTransformAnimator)
                _rectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();

            if (!_imageAnimator)
                _imageAnimator = GetComponentInChildren<UIImageAnimator>();
        }
        
        public override void Enter()
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Enter(_animatedElementSettings._enterDuration, _enterDelay));
        }

        IEnumerator Enter(float duration, float delay)
        {
            if (_rectTransformAnimator == null || _imageAnimator == null)
                yield break;
            
            _rectTransformAnimator.SetAnchoredPosition3D(_animatedElementSettings._enterFrom);
            _imageAnimator.SetAlpha(_animatedElementSettings._enterFromAlpha);
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._enterTo, duration);
            _imageAnimator.ChangeAlpha(_animatedElementSettings._enterToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;
        }

        public override void Exit()
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Exit(_animatedElementSettings._exitDuration, _exitDelay));
        }

        IEnumerator Exit(float duration, float delay)
        {
            if (_rectTransformAnimator == null || _imageAnimator == null)
                yield break;
            
            yield return new WaitForSeconds(delay);
            _rectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._exitTo, duration);
            _imageAnimator.ChangeAlpha(_animatedElementSettings._exitToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;
        }

        void OnValidate()
        {
            if (!_rectTransformAnimator)
                _rectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();
            
            if (!_imageAnimator)
                _imageAnimator = GetComponentInChildren<UIImageAnimator>();

            _enterDelay = Mathf.Max(_enterDelay, 0f);
            _exitDelay = Mathf.Max(_exitDelay, 0f);
        }
    }
}
