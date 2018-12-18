using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;

namespace BH.UI
{
    [AddComponentMenu("UI/BH.UI - UIButton")]
    public class UIButton : UIAnimatedElement, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [MenuItem("GameObject/UI/BH.UI - UIButton")]
        static void CreateUIButton(MenuCommand menuCommand)
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

            GameObject button = new GameObject("BH.UI - UIButton");
            button.AddComponent<RectTransform>();
            Selection.activeObject = button;
            UIButton buttonUIButton = button.AddComponent(typeof(UIButton)) as UIButton;

            Undo.RegisterCreatedObjectUndo(button, "Create " + button.name);

            GameObject raycastImage = new GameObject("RaycastImage");
            raycastImage.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(raycastImage, button);
            Image raycastImageImage = raycastImage.AddComponent(typeof(Image)) as Image;
            raycastImageImage.color = Color.clear;

            Undo.RegisterCreatedObjectUndo(raycastImage, "Create " + raycastImage.name);

            GameObject animatedRectTransform = new GameObject("AnimatedRectTransform");
            animatedRectTransform.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(animatedRectTransform, button);
            buttonUIButton._buttonRectTransformAnimator = animatedRectTransform.AddComponent(typeof(UIRectTransformAnimator)) as UIRectTransformAnimator;

            Undo.RegisterCreatedObjectUndo(animatedRectTransform, "Create " + animatedRectTransform.name);

            GameObject animatedImage = new GameObject("AnimatedImage");
            animatedImage.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(animatedImage, animatedRectTransform);
            Image animatedImageImage = animatedImage.AddComponent(typeof(Image)) as Image;
            animatedImageImage.raycastTarget = false;
            buttonUIButton._buttonImageAnimator = animatedImage.AddComponent(typeof(UIImageAnimator)) as UIImageAnimator;

            Undo.RegisterCreatedObjectUndo(animatedImage, "Create " + animatedImage.name);

            GameObject animatedText = new GameObject("AnimatedText");
            animatedText.AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(animatedText, animatedRectTransform);
            TextMeshProUGUI animatedTextText = animatedText.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
            animatedTextText.raycastTarget = false;
            buttonUIButton._buttonTextAnimator = animatedText.AddComponent(typeof(UITMProTextAnimator)) as UITMProTextAnimator;

            Undo.RegisterCreatedObjectUndo(animatedText, "Create " + animatedText.name);

            // Check if object is being created with left click or right click.
            GameObject contextObject = menuCommand.context as GameObject;
            if (contextObject == null)
                GameObjectUtility.SetParentAndAlign(button, canvas.gameObject);
            else
                GameObjectUtility.SetParentAndAlign(button, contextObject);
            
            // Check if an event system already exists in the scene.
            if (!FindObjectOfType<EventSystem>())
            {
                GameObject eventObject = new GameObject("EventSystem", typeof(EventSystem));
                eventObject.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventObject, "Create " + eventObject.name);
            }
        }

        interface IButtonState
        {
            void OnPointerEnter();
            void OnPointerExit();
            void OnPointerDown();
            void OnPointerUp();
        }

        class IdleState : IButtonState
        {
            UIButton _button;

            public IdleState(UIButton button)
            {
                _button = button;
            }

            public void OnPointerEnter()
            {
                _button._currentState = _button._hoveredOverState;
                _button.OnButtonEnterInvoke();
                _button.AnimateHoveredOver();
            }

            public void OnPointerExit() {}

            public void OnPointerDown()
            {
                _button._currentState = _button._pressedDownState;
                _button.OnButtonDownInvoke();
                _button.AnimatePressedDown();
            }

            public void OnPointerUp() {}
        }

        class HoveredOverState : IButtonState
        {
            UIButton _button;

            public HoveredOverState(UIButton button)
            {
                _button = button;
            }

            public void OnPointerEnter() {}

            public void OnPointerExit()
            {
                _button._currentState = _button._idleState;    
                _button.OnButtonExitInvoke();
                _button.AnimateIdle();
            }

            public void OnPointerDown()
            {
                _button._currentState = _button._pressedDownState;
                _button.OnButtonDownInvoke();
                _button.AnimatePressedDown();
            }

            public void OnPointerUp() {}
        }

        class PressedDownState : IButtonState
        {
            UIButton _button;

            public PressedDownState(UIButton button)
            {
                _button = button;
            }

            public void OnPointerEnter() {}

            public void OnPointerExit()
            {
                _button._currentState = _button._idleState;
                _button.OnButtonExitInvoke();
                _button.AnimateIdle();
            }

            public void OnPointerDown() {}

            public void OnPointerUp()
            {
                _button._currentState = _button._hoveredOverState;
                _button.OnButtonUpInvoke();
                _button.AnimateHoveredOver();
            }
        }

        IButtonState _currentState;
        IButtonState _idleState;
        IButtonState _hoveredOverState;
        IButtonState _pressedDownState;

        [SerializeField] UIButtonRectTransformSettings _buttonRectTransformSettings;
        [SerializeField] UIButtonImageSettings _buttonImageSettings;
        [SerializeField] UIButtonTextSettings _buttonTextSettings;
        [SerializeField] UIButtonSoundEffects _buttonSoundEffects;

        [SerializeField] UnityEvent _onButtonDown;
        [SerializeField] UnityEvent _onButtonUp;
        [SerializeField] UnityEvent _onButtonEnter;
        [SerializeField] UnityEvent _onButtonExit;

        [SerializeField] protected UIRectTransformAnimator _buttonRectTransformAnimator;
        [SerializeField] protected UIImageAnimator _buttonImageAnimator;
        [SerializeField] protected UITMProTextAnimator _buttonTextAnimator;

        [SerializeField] protected Image _buttonRaycastImage;

        Coroutine _animation;

        void Awake()
        {
            OnValidate();

            _idleState = new IdleState(this);
            _hoveredOverState = new HoveredOverState(this);
            _pressedDownState = new PressedDownState(this);
            _currentState = _idleState;
        }

        public void OnPointerEnter(PointerEventData data)
        {
            _currentState.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData data)
        {
            _currentState.OnPointerExit();
        }

        public void OnPointerDown(PointerEventData data)
        {
            _currentState.OnPointerDown();
        }

        public void OnPointerUp(PointerEventData data)
        {
            _currentState.OnPointerUp();
        }
        
        public void OnButtonDownInvoke()
        {
            UIAudioManager.Instance.Play(_buttonSoundEffects._playOnButtonDown);
            if (_onButtonDown != null)
                _onButtonDown.Invoke();
        }

        public void OnButtonUpInvoke()
        {
            UIAudioManager.Instance.Play(_buttonSoundEffects._playOnButtonUp);
            if (_onButtonUp != null)
                _onButtonUp.Invoke();
        }

        public void OnButtonEnterInvoke()
        {
            UIAudioManager.Instance.Play(_buttonSoundEffects._playOnButtonEnter);
            if (_onButtonEnter != null)
                _onButtonEnter.Invoke();
        }

        public void OnButtonExitInvoke()
        {
            UIAudioManager.Instance.Play(_buttonSoundEffects._playOnButtonExit);
            if (_onButtonExit != null)
                _onButtonExit.Invoke();
        }

        public override void Enter(NoArgDelegate callback = null)
        {
            _currentState = _idleState;

            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Enter(_animatedElementSettings._enterDuration, _enterDelay, callback));
        }

        IEnumerator Enter(float duration, float delay, NoArgDelegate callback = null)
        {
            if (_buttonRectTransformAnimator == null || _buttonImageAnimator == null || _buttonTextAnimator == null)
                yield break;
            
            _buttonRectTransformAnimator.SetAnchoredPosition3D(_animatedElementSettings._enterFrom);
            _buttonImageAnimator.SetAlpha(_animatedElementSettings._enterFromAlpha);
            _buttonTextAnimator.SetAlpha(_animatedElementSettings._enterFromAlpha);
            yield return new WaitForSeconds(delay);
            _buttonRectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._enterTo, duration);
            _buttonImageAnimator.ChangeAlpha(Mathf.Min(_animatedElementSettings._enterToAlpha, _buttonImageSettings._idleColor.a), duration);
            _buttonTextAnimator.ChangeAlpha(Mathf.Min(_animatedElementSettings._enterToAlpha, _buttonTextSettings._idleColor.a), duration);
            yield return new WaitForSeconds(duration);
            _animation = null;

            if (callback != null)
                callback.Invoke();
        }

        public override void Exit(NoArgDelegate callback = null)
        {
            _currentState = _idleState;

            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(Exit(_animatedElementSettings._exitDuration, _exitDelay, callback));
        }

        IEnumerator Exit(float duration, float delay, NoArgDelegate callback = null)
        {
            if (_buttonRectTransformAnimator == null || _buttonImageAnimator == null || _buttonTextAnimator == null)
                yield break;
            
            yield return new WaitForSeconds(delay);
            _buttonRectTransformAnimator.ChangeAnchoredPosition3D(_animatedElementSettings._exitTo, duration);
            _buttonImageAnimator.ChangeAlpha(_animatedElementSettings._exitToAlpha, duration);
            _buttonTextAnimator.ChangeAlpha(_animatedElementSettings._exitToAlpha, duration);
            yield return new WaitForSeconds(duration);
            _animation = null;

            if (callback != null)
                callback.Invoke();
        }

        public void EnableRaycast()
        {
            _buttonRaycastImage.enabled = true;
        }

        public void DisableRaycast()
        {
            _buttonRaycastImage.enabled = false;
        }

        void AnimateIdle()
        {
            _buttonRectTransformAnimator.ChangeScale(_buttonRectTransformSettings._idleScale, _buttonRectTransformSettings._changeScaleDuration);
            _buttonRectTransformAnimator.ChangeAnchoredPosition3D(_buttonRectTransformSettings._idleAnchoredPosition3D, _buttonRectTransformSettings._changeAnchoredPosition3DDuration);
            _buttonImageAnimator.ChangeColor(_buttonImageSettings._idleColor, _buttonImageSettings._changeColorDuration);
            _buttonImageAnimator.ChangeScale(_buttonImageSettings._idleScale, _buttonImageSettings._changeScaleDuration);
            _buttonImageAnimator.ChangeAnchoredPosition3D(_buttonImageSettings._idleAnchoredPosition3D, _buttonImageSettings._changeAnchoredPosition3DDuration);
            _buttonTextAnimator.ChangeColor(_buttonTextSettings._idleColor, _buttonTextSettings._changeColorDuration);
            _buttonTextAnimator.ChangeScale(_buttonTextSettings._idleScale, _buttonTextSettings._changeScaleDuration);
            _buttonTextAnimator.ChangeAnchoredPosition3D(_buttonTextSettings._idleAnchoredPosition3D, _buttonTextSettings._changeAnchoredPosition3DDuration);
        }

        void AnimateHoveredOver()
        {
            _buttonRectTransformAnimator.ChangeScale(_buttonRectTransformSettings._hoveredOverScale, _buttonRectTransformSettings._changeScaleDuration);
            _buttonRectTransformAnimator.ChangeAnchoredPosition3D(_buttonRectTransformSettings._hoveredOverAnchoredPosition3D, _buttonRectTransformSettings._changeAnchoredPosition3DDuration);
            _buttonImageAnimator.ChangeColor(_buttonImageSettings._hoveredOverColor, _buttonImageSettings._changeColorDuration);
            _buttonImageAnimator.ChangeScale(_buttonImageSettings._hoveredOverScale, _buttonImageSettings._changeScaleDuration);
            _buttonImageAnimator.ChangeAnchoredPosition3D(_buttonImageSettings._hoveredOverAnchoredPosition3D, _buttonImageSettings._changeAnchoredPosition3DDuration);
            _buttonTextAnimator.ChangeColor(_buttonTextSettings._hoveredOverColor, _buttonTextSettings._changeColorDuration);
            _buttonTextAnimator.ChangeScale(_buttonTextSettings._hoveredOverScale, _buttonTextSettings._changeScaleDuration);
            _buttonTextAnimator.ChangeAnchoredPosition3D(_buttonTextSettings._hoveredOverAnchoredPosition3D, _buttonTextSettings._changeAnchoredPosition3DDuration);
        }

        void AnimatePressedDown()
        {
            _buttonRectTransformAnimator.ChangeScale(_buttonRectTransformSettings._pressedDownScale, _buttonRectTransformSettings._changeScaleDuration);
            _buttonRectTransformAnimator.ChangeAnchoredPosition3D(_buttonRectTransformSettings._pressedDownAnchoredPosition3D, _buttonRectTransformSettings._changeAnchoredPosition3DDuration);
            _buttonImageAnimator.ChangeColor(_buttonImageSettings._pressedDownColor, _buttonImageSettings._changeColorDuration);
            _buttonImageAnimator.ChangeScale(_buttonImageSettings._pressedDownScale, _buttonImageSettings._changeScaleDuration);
            _buttonImageAnimator.ChangeAnchoredPosition3D(_buttonImageSettings._pressedDownAnchoredPosition3D, _buttonImageSettings._changeAnchoredPosition3DDuration);
            _buttonTextAnimator.ChangeColor(_buttonTextSettings._pressedDownColor, _buttonTextSettings._changeColorDuration);
            _buttonTextAnimator.ChangeScale(_buttonTextSettings._pressedDownScale, _buttonTextSettings._changeScaleDuration);
            _buttonTextAnimator.ChangeAnchoredPosition3D(_buttonTextSettings._pressedDownAnchoredPosition3D, _buttonTextSettings._changeAnchoredPosition3DDuration);
        }

        void OnValidate()
        {
            if (!_buttonRectTransformAnimator)
                _buttonRectTransformAnimator = GetComponentInChildren<UIRectTransformAnimator>();

            if (_buttonRectTransformAnimator)
            {
                _buttonRectTransformAnimator.SetScale(_buttonRectTransformSettings._idleScale);
                _buttonRectTransformAnimator.SetAnchoredPosition3D(_buttonRectTransformSettings._idleAnchoredPosition3D);
            }

            if (!_buttonImageAnimator)
                _buttonImageAnimator = GetComponentInChildren<UIImageAnimator>();

            if (_buttonImageAnimator)
            {
                _buttonImageAnimator.SetColor(_buttonImageSettings._idleColor);
                _buttonImageAnimator.SetScale(_buttonImageSettings._idleScale);
                _buttonImageAnimator.SetAnchoredPosition3D(_buttonImageSettings._idleAnchoredPosition3D);
            }

            if (!_buttonTextAnimator)
                _buttonTextAnimator = GetComponentInChildren<UITMProTextAnimator>();

            if (_buttonTextAnimator)
            {
                _buttonTextAnimator.SetColor(_buttonTextSettings._idleColor);
                _buttonTextAnimator.SetScale(_buttonTextSettings._idleScale);
                _buttonTextAnimator.SetAnchoredPosition3D(_buttonTextSettings._idleAnchoredPosition3D);
            }
            
            if (!_buttonRaycastImage || !_buttonRaycastImage.raycastTarget)
            {
                List<Image> images = GetComponentsInChildren<Image>().ToList();
                _buttonRaycastImage = images.First(i => i.raycastTarget);
            }

            _enterDelay = Mathf.Max(_enterDelay, 0f);
            _exitDelay = Mathf.Max(_exitDelay, 0f);
        }
    }
}
