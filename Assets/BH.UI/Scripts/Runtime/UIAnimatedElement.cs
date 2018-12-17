using UnityEngine;

namespace BH.UI
{
    public abstract class UIAnimatedElement : MonoBehaviour
    {
        [SerializeField] protected Vector3 _enterFrom = Vector3.down * 100f;
        [SerializeField] protected Vector3 _enterTo = Vector3.zero;
        protected float _enterFromAlpha = 0f;
        protected float _enterToAlpha = 1f;
        [SerializeField] protected float _enterDuration = 1f;
        [SerializeField] protected float _enterDelay = 0f;
        [SerializeField] protected Vector3 _exitTo = Vector3.down * 100f;
        [SerializeField] protected float _exitDuration = 1f;
        [SerializeField] protected float _exitDelay = 0f;
        protected bool _isAnimating = false;

        public abstract void Enter();
        public abstract void Exit();
    }
}
