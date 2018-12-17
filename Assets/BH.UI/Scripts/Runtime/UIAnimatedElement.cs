using UnityEngine;

namespace BH.UI
{
    public abstract class UIAnimatedElement : MonoBehaviour
    {
        [SerializeField] protected UIAnimatedElementSettings _animatedElementSettings;
        [SerializeField] protected float _enterDelay = 0f;
        [SerializeField] protected float _exitDelay = 0f;
        protected bool _isAnimating = false;

        public abstract void Enter();
        public abstract void Exit();
    }
}
