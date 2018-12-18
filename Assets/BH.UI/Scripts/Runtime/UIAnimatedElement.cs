using UnityEngine;

namespace BH.UI
{
    public abstract class UIAnimatedElement : MonoBehaviour
    {
        [SerializeField] protected UIAnimatedElementSettings _animatedElementSettings;
        public UIAnimatedElementSettings AnimatedElementSettings
        {
            get { return _animatedElementSettings; }
            private set { }
        }

        [SerializeField] protected float _enterDelay = 0f;
        public float EnterDelay
        {
            get { return _enterDelay; }
            private set { }
        }

        [SerializeField] protected float _exitDelay = 0f;
        public float ExitDelay
        {
            get { return _exitDelay; }
            private set { }
        }

        protected bool _isAnimating = false;

        public abstract void Enter();
        public abstract void Exit();
    }
}
