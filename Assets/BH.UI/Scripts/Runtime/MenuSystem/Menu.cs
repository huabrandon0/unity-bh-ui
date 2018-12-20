using UnityEngine;

namespace BH.UI
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        public static T Instance { get; private set; }
        protected static bool _isClosing = false;
        public static bool IsClosing
        {
            get { return _isClosing; }
            private set { _isClosing = value; }
        }

        protected virtual void Awake()
        {
            Instance = this as T;

            if (!_animatedElementOverlay)
                _animatedElementOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        protected static void Open(bool animate = true, NoArgDelegate callback = null)
        {
            if (IsClosing)
                return;

            if (Instance == null)
                MenuManager.Instance.CreateInstance<T>();
            else
                Instance.gameObject.SetActive(true);

            MenuManager.Instance.OpenMenu(Instance, animate, callback);
        }

        protected static bool Close(bool animate = true, NoArgDelegate callback = null)
        {
            if (Instance == null)
            {
                //Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", typeof(T));
                return false;
            }
            else if (IsClosing)
            {
                return true;
            }

            IsClosing = true;
            return MenuManager.Instance.CloseMenu(Instance, animate, () =>
            {
                IsClosing = false;
                if (callback != null)
                    callback.Invoke();
            });
        }

        public override void OnBackPressed()
        {
            Close();
        }
    }

    public abstract class Menu : MonoBehaviour
    {
        public UIAnimatedElementOverlay _animatedElementOverlay;

        [Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
        public bool DestroyWhenClosed = true;

        [Tooltip("Disable menus that are under this one in the stack")]
        public bool DisableMenusUnderneath = true;

        public abstract void OnBackPressed();

        protected virtual void OnValidate()
        {
            if (!_animatedElementOverlay)
                _animatedElementOverlay = GetComponentInChildren<UIAnimatedElementOverlay>();
        }
    }
}
