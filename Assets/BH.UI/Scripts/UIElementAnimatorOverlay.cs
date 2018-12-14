//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//namespace BH.UIAnimation
//{
//    public class UIElementAnimatorOverlay : MonoBehaviour
//    {
//        List<IUIElementAnimator> _elementAnimators;
        
//        [SerializeField] bool _defaultOn = true;

//        void Awake()
//        {
//            _elementAnimators = GetComponentsInChildren<IUIElementAnimator>().ToList();

//            if (_defaultOn)
//            {
//                foreach (IUIElementAnimator elementAnimator in _elementAnimators)
//                {
//                    elementAnimator.IdleOn();
//                }
//            }
//            else
//            {
//                foreach (IUIElementAnimator elementAnimator in _elementAnimators)
//                {
//                    elementAnimator.IdleOff();
//                }
//            }
//        }

//        public void Enter()
//        {
//            foreach (IUIElementAnimator elementAnimator in _elementAnimators)
//            {
//                elementAnimator.Enter();
//            }
//        }

//        public void Exit()
//        {
//            foreach (IUIElementAnimator elementAnimator in _elementAnimators)
//            {
//                elementAnimator.Exit();
//            }
//        }
//    }
//}
