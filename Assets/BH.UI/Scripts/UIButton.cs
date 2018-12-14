﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BH.UI
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
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
                _button._buttonAnimator.ChangeColor(_button._hoveredOverColor, _button._changeColorDuration);
                _button._buttonAnimator.ChangeScale(_button._hoveredOverScale, _button._changeScaleDuration);
                _button._buttonAnimator.ChangeLocalPosition(_button._hoveredOverLocalPosition, _button._changeLocalPositionDuration);
                UIAudioManager.Instance.Play(_button._playOnButtonEnter);
            }

            public void OnPointerExit() {}

            public void OnPointerDown()
            {
                _button._currentState = _button._pressedDownState;
                _button.OnButtonDownInvoke();
                _button._buttonAnimator.ChangeColor(_button._pressedDownColor, _button._changeColorDuration);
                _button._buttonAnimator.ChangeScale(_button._pressedDownScale, _button._changeScaleDuration);
                _button._buttonAnimator.ChangeLocalPosition(_button._pressedDownLocalPosition, _button._changeLocalPositionDuration);
                UIAudioManager.Instance.Play(_button._playOnButtonDown);
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
                _button._buttonAnimator.ChangeColor(_button._idleColor, _button._changeColorDuration);
                _button._buttonAnimator.ChangeScale(_button._idleScale, _button._changeScaleDuration);
                _button._buttonAnimator.ChangeLocalPosition(_button._idleLocalPosition, _button._changeLocalPositionDuration);
                UIAudioManager.Instance.Play(_button._playOnButtonExit);
            }

            public void OnPointerDown()
            {
                _button._currentState = _button._pressedDownState;
                _button.OnButtonDownInvoke();
                _button._buttonAnimator.ChangeColor(_button._pressedDownColor, _button._changeColorDuration);
                _button._buttonAnimator.ChangeScale(_button._pressedDownScale, _button._changeScaleDuration);
                _button._buttonAnimator.ChangeLocalPosition(_button._pressedDownLocalPosition, _button._changeLocalPositionDuration);
                UIAudioManager.Instance.Play(_button._playOnButtonDown);
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
                _button._buttonAnimator.ChangeColor(_button._idleColor, _button._changeColorDuration);
                _button._buttonAnimator.ChangeScale(_button._idleScale, _button._changeScaleDuration);
                _button._buttonAnimator.ChangeLocalPosition(_button._idleLocalPosition, _button._changeLocalPositionDuration);
                UIAudioManager.Instance.Play(_button._playOnButtonExit);
            }

            public void OnPointerDown() {}

            public void OnPointerUp()
            {
                _button._currentState = _button._hoveredOverState;
                _button.OnButtonUpInvoke();
                _button._buttonAnimator.ChangeColor(_button._hoveredOverColor, _button._changeColorDuration);
                _button._buttonAnimator.ChangeScale(_button._hoveredOverScale, _button._changeScaleDuration);
                _button._buttonAnimator.ChangeLocalPosition(_button._hoveredOverLocalPosition, _button._changeLocalPositionDuration);
                UIAudioManager.Instance.Play(_button._playOnButtonUp);
            }
        }

        IButtonState _currentState;
        IButtonState _idleState;
        IButtonState _hoveredOverState;
        IButtonState _pressedDownState;

        [SerializeField] Color _idleColor = Color.white;
        [SerializeField] Color _hoveredOverColor = Color.gray;
        [SerializeField] Color _pressedDownColor = Color.black;
        [SerializeField] float _changeColorDuration = 0.1f;

        [SerializeField] float _idleScale = 1f;
        [SerializeField] float _hoveredOverScale = 1.1f;
        [SerializeField] float _pressedDownScale = 0.9f;
        [SerializeField] float _changeScaleDuration = 0.1f;

        [SerializeField] Vector3 _idleLocalPosition = Vector3.zero;
        [SerializeField] Vector3 _hoveredOverLocalPosition = Vector3.zero;
        [SerializeField] Vector3 _pressedDownLocalPosition = Vector3.zero;
        [SerializeField] float _changeLocalPositionDuration = 0.1f;

        [SerializeField] AudioClip _playOnButtonDown;
        [SerializeField] AudioClip _playOnButtonUp;
        [SerializeField] AudioClip _playOnButtonEnter;
        [SerializeField] AudioClip _playOnButtonExit;

        [SerializeField] UnityEvent _onButtonDown;
        [SerializeField] UnityEvent _onButtonUp;
        [SerializeField] UnityEvent _onButtonEnter;
        [SerializeField] UnityEvent _onButtonExit;

        [SerializeField] UIImageAnimator _buttonAnimator;

        void Awake()
        {
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
            if (_onButtonDown != null)
                _onButtonDown.Invoke();
        }

        public void OnButtonUpInvoke()
        {
            if (_onButtonUp != null)
                _onButtonUp.Invoke();
        }

        public void OnButtonEnterInvoke()
        {
            if (_onButtonEnter != null)
                _onButtonEnter.Invoke();
        }

        public void OnButtonExitInvoke()
        {
            if (_onButtonExit != null)
                _onButtonExit.Invoke();
        }
    }
}
